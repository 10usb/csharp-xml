using System;
using System.Collections.Generic;

namespace Sunit.Xml {
  /// <summary>
  /// An empty state to be used as base class
  /// </summary>
  public class DefaultState : IState {
    /// <summary>
    /// Should be overridden by the subclass when an action has to be done when a close tag is found
    /// </summary>
    /// <param name="qualifiedName"></param>
    public virtual void OnClose(QualifiedName qualifiedName) { }

    /// <summary>
    /// Should be overridden by the subclass when an action has to be done when a comment tag is found
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnComment(string value) { }

    /// <summary>
    /// Should be overridden by the subclass when an action has to be done when a element is found
    /// </summary>
    /// <param name="qualifiedName"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public virtual IState OnElement(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) {
      return new DefaultState();
    }

    /// <summary>
    /// Should be overridden by the subclass when an the info of the name or arrtibutes are relevent
    /// </summary>
    /// <param name="qualifiedName"></param>
    /// <param name="attributes"></param>
    public virtual void OnOpen(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) { }

    /// <summary>
    /// Should be overridden by the subclass when an action has to be done when a text node is found
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnText(string value) { }
  }
}
