using System;
using System.Xml;

namespace Sunit.Xml {
  /// <summary>
  /// Respresents a fully qualified name of a tag or attribute, composed by a name and unique resource identifier
  /// </summary>
  public class QualifiedName : IEquatable<QualifiedName> {
    /// <summary>
    /// Characters not allowed to be in the name
    /// </summary>
    private static readonly char[] invalidChars = new char[] { ':', '<', '>', '=', ' ', '&' };

    /// <summary>
    /// Name part of the qualified name
    /// </summary>
    private string name;

    /// <summary>
    /// Uri part of the qualified name
    /// </summary>
    private string uri;

    /// <summary>
    /// Name part of the qualified name
    /// </summary>
    public string Name {
      get { return name; }
    }

    /// <summary>
    /// Uri part of the qualified name
    /// </summary>
    public string Uri {
      get { return uri; }
    }

    /// <summary>
    /// Constructs a qualified name instance with only the name part
    /// </summary>
    /// <param name="name"></param>
    private QualifiedName(string name) {
      this.name = name;
      this.uri = null;
    }

    /// <summary>
    /// Constructs a qualified name instance
    /// </summary>
    /// <param name="name"></param>
    /// <param name="uri"></param>
    private QualifiedName(string name, string uri) {
      this.name = name;
      this.uri = string.IsNullOrWhiteSpace(uri) ? null : uri;
    }

    /// <summary>
    /// To tell if this qualified name is equal to a given name and no uri
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Equals(string name) {
      return this.name == name && uri == null;
    }

    /// <summary>
    /// To tell if this qualified name is equal to a given name and uri
    /// </summary>
    /// <param name="name"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public bool Equals(string name, string uri) {
      return this.name == name && this.uri == uri;
    }

    /// <summary>
    /// To tell if two instances of a qualified name are equal
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(QualifiedName other) {
      if (other is null) return false;
      return Equals(other.name, other.uri);
    }

    /// <summary>
    /// To tell if this intance is equal to the other object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj) {
      if (obj is null) return false;
      QualifiedName other = obj as QualifiedName;
      return Equals(other.name, other.uri);
    }

    /// <summary>
    /// Returns the hash of this qualified name
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() {
      return ToString().GetHashCode();
    }

    /// <summary>
    /// Returns a string representation of this qualified name
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
      if (uri is null) return name;
      return string.Format("{0}:{1}", name, uri);
    }

    /// <summary>
    /// Creates a new instance of a qualified name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static QualifiedName Create(string name, string uri) {
      if (name is null) new Exception("Name can't be null");
      if (name.IndexOfAny(invalidChars) >= 0) new Exception("Invalid name");
      return new QualifiedName(name, uri);
    }

    /// <summary>
    /// Implicit operator extracting qualified name out of a XmlReader
    /// </summary>
    /// <param name="reader"></param>
    public static implicit operator QualifiedName(XmlReader reader) {
      return new QualifiedName(reader.LocalName, reader.NamespaceURI);
    }

    /// <summary>
    /// Implicit operator overloading allowing a string to be converted to qualified name with no uri
    /// </summary>
    /// <param name="name"></param>
    public static implicit operator QualifiedName(string name) {
      return new QualifiedName(name, null);
    }
  }
}
