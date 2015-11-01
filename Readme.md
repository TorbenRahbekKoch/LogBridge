[![Build status](https://ci.appveyor.com/api/projects/status/niturj0ljaimfmt0/branch/master?svg=true)](https://ci.appveyor.com/project/TorbenRahbekKoch/logbridge/branch/master)

LogBridge
---------

A no-nonsense logging wrapper aiming to give a consistent and versatile interface
to various logging frameworks, such as Log4Net and Enterprise Library.

LogBridge tries as hard as possible never to
throw exceptions in any logging method. Instead it will continue but may
instead log an incomplete message.

Please see the Configuration section below for details about this.

Installation
=====

Install in the main application (e.g. .exe or web application) with Nuget:

-  [Install-package SoftwarePassion.LogBridge.EnterpriseLibrary](https://www.nuget.org/packages/SoftwarePassion.LogBridge.EnterpriseLibrary/)
-  [Install-package SoftwarePassion.LogBridge.Log4Net](https://www.nuget.org/packages/SoftwarePassion.LogBridge.Log4Net/)
-  [Install-package SoftwarePassion.LogBridge.UmbracoLog4Net](https://www.nuget.org/packages/SoftwarePassion.LogBridge.UmbracoLog4Net/)

There is a special package for Umbraco since it uses a custom build of Log4net.

In supporting libraries you can simply restrict yourself to installing the basic LogBridge library
with Nuget:

 - [LogBridge](http://www.nuget.org/packages/SoftwarePassion.LogBridge/)

Usage
=====

### Logging levels

There are five logging levels, which correspond to Log4Net and Enterprise Library as follows:

LogBridge   | Log4Net | Enterprise Library (Severity Level) 
-----------------------------------------------------------
Information | Info    | Information 
Debug       | Debug   | Verbose
Warning     | Warn    | Warning
Error       | Error   | Error
Fatal       | Fatal   | Critical

To use the logger, simply put in a using statement:

`using SoftwarePassion.LogBridge;`

And call any of the static methods on the Log class, e.g

`Log.Information("This is an informational message only.");`

Each level has the exact same overloads available. You can also use formatting
parameters like this:

 `Log.Information("This is a {0} formatted message.", "neatly");`

It works in the same way as
[string.Format()](http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.110%29.aspx)
does. It uses
[CultureInfo.InvariantCulture](http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.invariantculture%28v=vs.110%29.aspx)
as the formatting provider - this will likely be configurable in a later version.

You can easily log an exception:

`Log.Warning(exception);`

There are some overloads which have a parameter called `firstMessageParameter`.
This parameter is only there to help with overload resolution and should,
usage-wise, be considered the same method as the one without this parameter.
Perhaps C# will render this unnecessary in a later version.

### Describing Parameters

Logging the value of the parameters to a method can be very useful when
e.g. catching exceptions. To aid with that there is a static class *Describe*, which
has a method *Parameters*. Calling this method with the parameters to the
method in the exact same order will give back a string which contains a
rather comprehensive description of the parameters.

`Describe.Parameters(parm1, parm2, parm3)`

It would, probably, be technically feasible to automatically detect the
parameters and get the values, but this would include connecting to the
debugger API, which would be prohibitively expensive and probably also
required elevated priviliges.

The *Describe* class also has a method *CreateDescriber*, which is very helpful
in scenarios where one is running stuff inside a lambda-expression. E.g.

```

void MyMethod(int parm1, string parm2)
{

    var descriptor = Describe.CreateDescriptor(parm1, parm2);

    var lambda = () =>
    {
        try
        {
            // Do something exceptional
        }
        catch (Exception e)
        {
            Log.Warning(e, descriptor().Tostring());
        }
    }

    lambda();
}
```


*CreateDescriber* captures the MethodInfo for the method from which it is 
called, which makes it possible to properly describe the parameters for the 
method in question.

And why couldn't you just call `Describe.Parameters(...)` within the catch
phrase? This is because the C# compiler generates (behind your back) a
class with a method, which contains the actual code for the lambda. And if
you just called `Describe.Parameters(...)` from within that method the 
name of the method would be something weird with *DisplayClass* in it. Not
a big issue ofcourse, but having the actual method name is quite helpful
when logging errors.

### LogLocation

In general, LogBridge is perfectly able to figure out from where the Log 
statement is called, but this sometimes does not work very well, when 
called from within a lambda-closure. LogBridge finds the location by
walking the stack until it hits something, which is NOT LogBridge.

It gets the location right, so to speak, within the code, which the
compiler generates from the lambda-closure, but this is generally something
close to unreadable. 

To totally and accurately pinpoint the location you can use 
`LogLocation.Here()`:

`Log.Information(LogLocation.Here(), "Just logging an informational message");`

`LogLocation.Here()` uses the *[CallerFilePath](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callerfilepathattribute%28v=vs.110%29.aspx)*, 
*[CallerMemberName](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callermembernameattribute%28v=vs.110%29.aspx)* and 
*[CallerLineNumber](https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.callerlinenumberattribute%28v=VS.110%29.aspx)* attributes 
which makes the compiler insert the information - on compile time.

This is, by the way, substantially faster - it takes roughly 1/5 of the time 
compared to having LogBridge figuring out the position itself, so if you are 
concerned about performance this is a very good method.

LogLocation is new from 1.3.

### Extended Properties

To ease the possibility of adding varying properties to a log-message, LogBridge
provides the concept of an extended-properties-object.

Several overloads on the Log class takes an object parameter, called
extendedProperties. Each *property* on this object is added to a
Dictionary<string, object>
    which is then passed on to the individual
    log-provider. What that log-provider does with it is, of course, implementation
    dependent.

Only one type of property gets a special treatment:

`public Guid CorrelationId {get;}`

If an extendedProperties object has one of these (case-insensitive) this may be
used as the CorrelationId for the log message. If more properties with a
CorrelationId (in varying case) is present, it is undefined which one is used.

If the property is not of type Guid, it is ignored.

Please read about CorrelationId below.

#### Adding Extended Properties in application/web configuration file.

Application-wide properties like e.g. ApplicationName can be added in the 
configuration file:

  <pre>
  &lt;logBridge logWrapperType="SoftwarePassion.LogBridge.Log4Net.Log4NetWrapper"
             logWrapperAssembly="LogBridge.Log4Net, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null"
             throwOnResolverFail="true"
             internalDiagnosticsEnabled="true"&gt;
    &lt;extendedProperties&gt;
      &lt;add name="Property1" value="Value1"/&gt;
      &lt;add name="Property2" value="Value2"/&gt;
    &lt;/extendedProperties&gt;
  &lt;/logBridge&gt;
  </pre>

CorrelationId
=============
To make it possible to correlate logging messages there are several ways to
indicate a *CorrelationId*. This CorrelationId (which is a Guid) can be handled
by the log-provider and e.g. stored in a database together with other
logging-information to make it possible to group log-messages by CorrelationId.

Several of the Log.* methods take a CorrelationId as the first parameter. This
way of providing a CorrelationId takes precedence over all other ways.

There are in total five ways to provide a CorrelationId - on order of precedence
they are:

1. Explicit correlationId parameter in Log.* methods.
2. Guid CorrelationId property on extensionProperties object.
3. ThreadCorrelationId - if assigned.
4. AppDomainCorrelationId - if assigned.
5. ProcessCorrelationId - if assinged.

If none of these are assigned, no CorrelationId will be added to the properties
of the log message.

Configuration
=============
Currently the configuration is very simple. In theory (and even, at  times, in
practice) LogBridge should be able to automatically find an implementation of
LogWrapper<> and load that. This presupposes that the assembly has been loaded
into the application. Simply having the assembly in the application directory
does not suffice, you also have to call code in the assembly to make .NET
load the assembly.

To get over this problem you simply have to state in which
assembly the log-wrapper is located. This is done using an *appSetting* called
*SoftwarePassion.LogBridge.LogWrapperAssembly*. Out of the box only the three
values are supported:

    - LogBridge.Log4Net, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null
    - LogBridge.UmbracoLog4Net, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null
    - LogBridge.EnterpriseLibrary, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null

The meaning of these should be self-evident. What this value does internally is
to have LogBridge manually load the Assembly and thereby making it available
for searching for implementations of LogWrapper<>.

You can also be more specific and set the *appSetting* **SoftwarePassion.LogBridge.LogWrapperType**
to the *full name* of the type.

### Version 1.2 changes

From version 1.2 LogBridge have a real configuration section, which means you
have to register a configuration section:

```
<configSections>
    <section name="logBridge" type="SoftwarePassion.LogBridge.LogBridgeConfigurationSection, LogBridge"/>
</configSections>
```

A configuration section can look like this - with the entire set of settings supported
in 1.2: 

```
<logBridge logWrapperType="SoftwarePassion.LogBridge.Tests.Unit.TestLogWrapper"
            logWrapperAssembly="LogBridge.Log4Net, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null"
            throwOnResolverFail="true"
            internalDiagnosticsEnabled="true">
<extendedProperties>
    <add name="Key" value="Value"/>
</extendedProperties>
</logBridge>
```

All properties are optional. 

One useful property to set would be *ApplicationName*, but any other property
can be set at will.

### Exceptions

Since LogBridge aims at, by default, to not throw exceptions you may be in a
situation where it is difficult to figure out why logging does not occur.

To figure out whether LogBridge is at fault, you can do two things:

### 1. Enable internal logging

- Set the property *internalDiagnosticsEnabled* on the *logBridge* configuration element to *true*.
- Enable tracing to a file:

```
<system.diagnostics>
<trace autoflush="true">
<listeners>
<add
name="textWriterTraceListener"
type="System.Diagnostics.TextWriterTraceListener"
initializeData="c:\log\logbridge.txt" />
<remove name="Default" />
</listeners>
</trace>
</system.diagnostics>
```

If, for some reason, LogBrige cannot find a log-wrapper, this will now be stated
in the Debug View and the above *logbridge.txt* file.

In general, property *internalDiagnosticsEnabled* on the *logBridge* configuration element to *true*, will make
LogBridge log internal exceptions using [`Trace.WriteLine(...)`](http://msdn.microsoft.com/en-us/library/system.diagnostics.trace.writeline%28v=vs.110%29.aspx)
which, among other configurable places, can be seen in the Visual Studio
Debugging Output View.

### 2. Enable exception on resolver fail

You can, also, urge LogBridge to throw an exception when it cannot find a
log wrapper. This is done by setting the property *throwOnResolverFail*
to *true*.

This can be helpful, if you early in the application make a simple log message
stating that the application has started. You can wrap this in a try-catch:

```
try
{
    Log.Information("Application started.");
}
catch (TypeInitializationException)
{
    // Handle problem...
}
```

This is the only place that LogBridge will ever throw an exception.

### Log4Net specifics

#### Initialization.

It may be necessary to call `log4net.Config.XmlConfigurator.Configure();` to initialize
Log4Net. You can also, as usual with Log4Net use the XmlConfigurator attribute.
[The Log4Net documentation has details about this](http://logging.apache.org/log4net/release/manual/configuration.html).

#### Logger discovery

Log4Net offers the nice feature to have a logger per e.g. class or namespace.
Unfortunately you normally have to instantiate those loggers yourself, in code.
[See Log4Net documentation for details about how this is done](http://logging.apache.org/log4net/release/faq.html).

LogBridge removes that necessity. You should configure the loggers in the
applications configuration file as you normally should, but LogBridge automatically
finds the most specific logger, by looking at where the Log.XXX method is called.

That is, it first looks at the fully qualified name of the class, if no logger
is found in then removes the class name and looks at the namespace. If continues
to remove the right-most parts of the namespace until it runs out or a logger
is found.

If no specific logger is found, `Log4NetWrapper` uses the root logger, which
always should be configured in Log4Net.

If you want to see how it is done, you can look in the `Log4Net.PerformGetLogger` method.

#### Properties

LogBridge.Log4Net adds the following properties to the Log4Net Properties Dictionary:

- correlationId
- eventId
- machineName
- applicationName
- processName
- exception


### Running Unit Tests

Since LogBridge depends a lot on being able to load the correct LogWrapper it tends
to fail the unit tests when they are all run in one AppDomain. So if you're using
e.g. Resharper to run the unit tests - you need to enable the option to run each
assembly in a separate AppDomain.
