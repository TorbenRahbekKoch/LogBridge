LogBridge
---------

Aiming to give a consistent and versatile interface to various logging
frameworks, starting with Log4Net, and Enterprise Library.

LogBridge is a no-nonsense logging wrapper, trying as hard as possible never to
throw exceptions in any logging method. Instead it will continue but may 
instead log an incomplete message.

Please see the Configuration section below for details about this.

Usage
=====

Install with Nuget:

  [EnterpriseLibrary](https://www.nuget.org/packages/SoftwarePassion.LogBridge.EnterpriseLibrary/)
  [Log4Net](https://www.nuget.org/packages/SoftwarePassion.LogBridge.Log4Net/)

There are five logging levels available:

- Information
- Debug
- Warning
- Error
- Fatal

The exact meaning and ordering (if any) of these is defined by the individual 
log-provider, e.g. Enterprise Library or Log4Net.

Logging in your application is done by simply calling any of the 5*18 
(logging-levels * overloads) static methods on the Log class.

E.g.

´Log.Information("This is a informational message only.");´

Each level has the exact same overloads available. You can also use formatting
parameters like this:

´Log.Information("This is a {0} formatted message.", "neatly");´

It works in the same way as 
[string.Format()](http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.110%29.aspx) 
does. It uses 
[CultureInfo.InvariantCulture](http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.invariantculture%28v=vs.110%29.aspx)
as the formatting provider.

You can easily log an exception:

´Log.Exception(exception);´

There are some overloads which have a parameter called ´firstMessageParameter´. 
This parameter is only there to help with overload resolution and should, 
usage-wise, be considered the same method as the one without this parameter.



Extended Properties
===================

To ease the possibility of adding varying properties to a log-message, LogBridge
provides the concept of an extended-properties-object.

Several overloads on the Log class takes an object parameter, called 
extendedProperties. Each *property* on this object is added to a 
Dictionary<string, object> which is then passed on to the individual 
log-provider. What that log-provider does with it is, of course, implementation
dependent.

Only on type of property gets a special treatment: 

´public Guid CorrelationId {get;}´

If an extendedProperties object has one of these (case-insensitive) this may be
used as the CorrelationId for the log message. If more properties with a 
CorrelationId (in varying case) is present, it is undefined which one is used.

If the property is not of type Guid, it is ignored.

Please read about CorrelationId below.

CorrelationId
=============
To make it possible to correlate logging messages there are several ways to
indicate a *CorrelationId*. This CorrelationId (which is a Guid) can be handled
by the log-provider and e.g. stored in a database together with other 
logging-information to make it possible to group log-messages by CorrelationId.

Several of the Log.* methods take a CorrelationId as the first parameter. This 
way of providing a CorrelationId takes precedence over all other ways.

There are in total give ways to provide a CorrelationId - on order of precedence
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
*SoftwarePassion.LogBridge.LogWrapperAssembly*. Out of the box only the two 
values are supported:

- LogBridge.Log4Net, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
- LogBridge.EnterpriseLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

The meaning of these should be self-evident. What this value does internally is 
to have LogBridge manually load the Assembly and thereby making it available 
for searching for implementations of LogWrapper<>.

### Exceptions

Since LogBridge aims at, by default, to not throw exceptions you may be in a 
situation where it is difficult to figure out why logging does not occur.

To figure out whether LogBridge is at fault, you can do two things:

### 1. Enable internal logging

- Set appSettings key *SoftwarePassion.LogBridge.InternalDiagnosticsEnabled* to *true*.
- Enable tracing to a file:

´´´´
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
´´´´

If, for some reason, LogBrige cannot find a log-wrapper, this will now be stated
in the Debug View and the above *logbridge.txt* file.

In general, setting the appSettings key 
*SoftwarePassion.LogBridge.InternalDiagnosticsEnabled* to *true*, will make 
LogBridge log internal exceptions using [´Trace.WriteLine(...)´](http://msdn.microsoft.com/en-us/library/system.diagnostics.trace.writeline%28v=vs.110%29.aspx)
which, among other configurable places, can be seen in the Visual Studio 
Debugging Output View.

### 2. Enable exception on resolver fail

You can, also, urge LogBridge to throw an exception when it cannot find a 
log wrapper. This is done by setting the appSettings key *SoftwarePassion.LogBridge.ThrowOnResolverFail*
to *true*.

This can be helpful, if you early in the application make a simple log message
stating that the application has started. You can wrap this in a try-catch:

´´´´
    try
    {
        Log.Information("Application started.");
    }
    catch (TypeInitializationException)
    {                
        // Handle problem...
    }
´´´´

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

That is, it first looks at the fully qualified name of the class, if not logger
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
- processName
- exception

Describe.Parameters
===================

Logging the value of the parameters to a method can be very useful when 
catching exceptions. To aid with that there is a static class *Describe*, which
has a method *Parameters*. Calling this method with the parameters to the 
method in the exact same order will give back a string which contains a
rather comprehensive description of the parameters.