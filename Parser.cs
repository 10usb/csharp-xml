using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Sunit.Xml {
  /// <summary>
  /// Parses all the node in an xml document and passes on the event to the current state
  /// </summary>
  public class Parser {
    /// <summary>
    /// Internal structure for tracking closing of elements
    /// </summary>
    struct Node {
      public QualifiedName Name;
      public IState State;
    }

    /// <summary>
    /// The XmlReader the data is pulled out
    /// </summary>
    XmlReader reader;

    /// <summary>
    /// Contains the current path to the current node
    /// </summary>
    Stack<Node> stack;

    /// <summary>
    /// The current node events will be triggerd on
    /// </summary>
    Node current;

    /// <summary>
    /// Buffer for the text content within a element wich could be multiple, if there are entities witin it
    /// </summary>
    StringBuilder buffer;

    /// <summary>
    /// Constructs an parser initialized with the initial state
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="state"></param>
    public Parser(XmlReader reader, IState state) {
      this.reader = reader;
      stack = new Stack<Node>();
      current = new Node {
        State = state
      };
    }

    /// <summary>
    /// Parses the content of the reader
    /// </summary>
    public void Parse() {
      while (reader.Read()) {
        switch (reader.NodeType) {
          case XmlNodeType.Element: {
            QualifiedName name = reader;
            bool isEmptyElement = reader.IsEmptyElement;
            Dictionary<QualifiedName, string> attributes = new Dictionary<QualifiedName, string>();
            if (reader.HasAttributes) {
              reader.MoveToFirstAttribute();
              do {
                attributes[reader] = reader.Value;
              } while (reader.MoveToNextAttribute());
            }

            if (buffer != null) {
              current.State.OnText(buffer.ToString());
              buffer = null;
            }

            Node next = new Node {
              Name = name,
              State = current.State.OnElement(name, attributes)
            };
            if (next.State is null) throw new Exception(string.Format("Unexpected element '{0}'", name));

            stack.Push(current);
            current = next;

            current.State.OnOpen(name, attributes);

            if (isEmptyElement) {
              current.State.OnClose(name);
              current = stack.Pop();
            }
          }
          break;
          case XmlNodeType.EndElement: {
            if (current.Name is null) throw new Exception("Unexpected close tag");

            QualifiedName name = reader;
            if (!current.Name.Equals(name)) throw new Exception(string.Format("Unexpected close tag '{0}' expected '{1}'", name, current.Name));

            if (buffer != null) {
              current.State.OnText(buffer.ToString());
              buffer = null;
            }

            current.State.OnClose(name);
            current = stack.Pop();
          }
          break;
          case XmlNodeType.Text: {
            if (buffer is null) {
              buffer = new StringBuilder(reader.Value);
            } else {
              buffer.Append(reader.Value);
            }
          }
          break;
          case XmlNodeType.Comment:
          case XmlNodeType.CDATA:
          case XmlNodeType.EntityReference: {
            throw new NotImplementedException();
          }
        }
      }
    }
  }
}
