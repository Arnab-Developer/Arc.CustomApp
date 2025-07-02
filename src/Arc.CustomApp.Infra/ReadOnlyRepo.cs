namespace Arc.CustomApp.Infra;

/// <summary>A read only repo which returns students data.</summary>
public interface IReadOnlyRepo
{
    /// <summary>Get students data.</summary>
    /// <returns>A task containing a collection of students data.</returns>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    public Task<IEnumerable<Student>> GetStudents(CancellationToken cancellationToken);

    /// <summary>Get student data by id.</summary>
    /// <param name="id">The id of the student.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing a student data.</returns>
    public Task<Student> GetStudentById(int id, CancellationToken cancellationToken);
}

/// <summary>A read only repo which returns students data for testing.</summary>
public class ReadOnlyRepo : IReadOnlyRepo
{
    /// <summary>Get students data for testing.</summary>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing a collection of students data.</returns>
    public async Task<IEnumerable<Student>> GetStudents(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        var students = new List<Student>()
        {
            new(1, "S1", "Sub1"),
            new(2, "S2", "Sub2"),
            new(3, "S3", "Sub3"),
            new(4, "S4", "Sub4")
        };

        return students;
    }

    /// <summary>Get student data by id for testing.</summary>
    /// <param name="id">The id of the student.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>A task containing a student data.</returns>
    public async Task<Student> GetStudentById(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        var student = new Student(id, $"{id} s3", "Sub3");
        return student;
    }
}
