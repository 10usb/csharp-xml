# C# Xml
This is a utility to process Xml files. The aim is to automate the usual state pattern build around reading the content into a structure data set of own design.

It works by setting an initial state that gets called when an element is found, and it has to return a new state (could be the same, not recommended)

# Events
- **OnOpen** Called by the parser when this state it set as current state
- **OnClose** Called by the parser when the closing tag of this state if found
- **OnElement** Called when an element is found, this method should return a new state. Best is not to return it self
- **OnText** Called by the parser when a text node is found
- **OnComment** Called by the parser when a comment element is found

# Example
```cs
using Sunit.Xml;

Parser parser = new Parser(stream, new MyInitialState());
parser.Parse();
```

```cs
  class MyInitialState : DefaultState {
    /* Some initialisation code... */
    public override IState OnElement(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) {
      if (qualifiedName.Equals("nameOfMyRootElement", "http://my.namespace.uri")) {
        return new /* Some state to handle the root element */;
      }
      return null;
    }
    /* The other events that can be triggerd */
  }
```
