using System;
using System.Collections.Generic;

namespace Sunit.Xml {
  public class DebugState : IState {
    public virtual void OnClose(QualifiedName qualifiedName) {
      Console.WriteLine("OnClose {0}", qualifiedName);
    }

    public virtual void OnComment(string value) {
      Console.WriteLine("OnComment {0}", value);
    }

    public virtual IState OnElement(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) {
      Console.WriteLine("OnElement {0}", qualifiedName);
      return new DebugState();
    }

    public virtual void OnOpen(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) {
      Console.WriteLine("OnOpen {0}", qualifiedName);
      foreach (KeyValuePair<QualifiedName, string> attribute in attributes) {
        Console.WriteLine("   {0} => {1}", attribute.Key, attribute.Value);
      }
    }

    public virtual void OnText(string value) {
      Console.WriteLine("OnText {0}", value);
    }
  }
}
