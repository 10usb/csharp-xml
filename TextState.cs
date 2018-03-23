using System;
using System.Collections.Generic;
using System.Text;

namespace Sunit.Xml {
  /// <summary>
  /// The standard state that collects any text within the current element recusively
  /// </summary>
  public class TextState : IState {
    /// <summary>
    /// The format of the delegate that will be called when the closing tag is found
    /// </summary>
    /// <param name="text"></param>
    public delegate void OnTextHandler(string text);

    /// <summary>
    /// Handler to be called when the closing tag is found
    /// </summary>
    OnTextHandler handler;

    /// <summary>
    /// StringBuiilder that collects each text node found
    /// </summary>
    StringBuilder stringBuilder;

    /// <summary>
    /// Construct a TextState with a handler that will be called when the closing tag is found
    /// </summary>
    /// <param name="handler"></param>
    public TextState(OnTextHandler handler) {
      if (handler is null) throw new ArgumentNullException("handler can't be null");
      this.handler = handler;
      stringBuilder = new StringBuilder();
    }
    
    /// <summary>
    /// Is ignored by the TextState
    /// </summary>
    /// <param name="qualifiedName"></param>
    /// <param name="attributes"></param>
    public void OnOpen(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) { }

    /// <summary>
    /// Will start a new TextState to collect any sub texts
    /// </summary>
    /// <param name="qualifiedName"></param>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public IState OnElement(QualifiedName qualifiedName, Dictionary<QualifiedName, string> attributes) {
      // TODO: pass on the StringBuilder
      return new TextState(delegate (string text) {
        stringBuilder.Append(text);
      });
    }

    /// <summary>
    /// Calles the handler when the closing tag is found
    /// </summary>
    /// <param name="qualifiedName"></param>
    public void OnClose(QualifiedName qualifiedName) {
      this?.handler(stringBuilder.ToString());
    }

    /// <summary>
    /// Addes any found text of a text node to the string being build
    /// </summary>
    /// <param name="value"></param>
    public void OnText(string value) {
      stringBuilder.Append(value);
    }

    /// <summary>
    /// Is ignored by the TextState
    /// </summary>
    /// <param name="value"></param>
    public void OnComment(string value) {}

  }
}
