namespace Arc.CustomApp.Infra;

/// <summary>A student model data.</summary>
/// <param name="Id">The id of the student.</param>
/// <param name="Name">The name of the student.</param>
/// <param name="Subject">The subject of the student.</param>
public record Student(int Id, string Name, string Subject);