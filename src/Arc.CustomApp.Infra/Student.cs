namespace Arc.CustomApp.Infra;

/// <summary>
/// Represents a student with an identifier, name, and subject of study.
/// </summary>
/// <remarks>This type is immutable and provides a concise way to store and retrieve student
/// information.</remarks>
/// <param name="Id">The id of the student.</param>
/// <param name="Name">The name of the student.</param>
/// <param name="Subject">The subject of the student.</param>
public record Student(int Id, string Name, string Subject);