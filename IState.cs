using System.Collections.Generic;

namespace Sunit.Xml {
  /// <summary>
  /// Every state the parser is in must impliment this interface to recieve it's events
  /// </summary>
  public interface IState {
    /// <summary>
    /// Called by the parser when this state it set as current state
    /// </summary>
    /// <param name="qualifiedName">Qualified name of the element</param>
    /// <param name="attributes">Attributes of the element</param>
    void OnOpen(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes);

    /// <summary>
    /// Called by the parser when the closing tag of this state if found
    /// </summary>
    /// <param name="qualifiedName"></param>
    void OnClose(QualifiedName qualifiedName);

    /// <summary>
    /// Called when an element is found, this method should return a new state. Best is not to return it self
    /// </summary>
    /// <param name="qualifiedName">Qualified name of the element</param>
    /// <param name="attributes">Attributes of the element</param>
    /// <returns></returns>
    IState OnElement(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes);

    /// <summary>
    /// Called bu the parser when a text node is found
    /// </summary>
    /// <param name="value">The resolved value of the text node</param>
    void OnText(string value);

    /// <summary>
    /// Called by the parser when a comment element is found
    /// </summary>
    /// <param name="value">The textual value of the comment</param>
    void OnComment(string value);
  }
}
