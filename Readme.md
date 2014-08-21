LogBridge
---------

Aiming to give a consistent and versatile interface to various logging
frameworks, starting with Log4Net and Enterprise Library.

LogBridge is a no-nonsense logging wrapper, trying as hard as possible never to
throw exceptions in any logging method. Instead it will continue but may 
instead log an incomplete message.

Usage
=====

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

�Log.Information("This is a informational message only.");�

Each level has the exact same overloads available. You can also use formatting
parameters like this:

�Log.Information("This is a {0} formatted message.", "neatly");�

It works in the same way as 
[string.Format()](http://msdn.microsoft.com/en-us/library/system.string.format%28v=vs.110%29.aspx) 
does. It uses 
[CultureInfo.InvariantCulture](http://msdn.microsoft.com/en-us/library/system.globalization.cultureinfo.invariantculture%28v=vs.110%29.aspx)
as the formatting provider.

You can easily log an exception:

�Log.Exception(exception);�

There are some overloads which have a parameter called �firstMessageParameter�. 
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

�public Guid CorrelationId {get;}�

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


