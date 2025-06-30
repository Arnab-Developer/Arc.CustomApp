namespace Arc.CustomApp.Infra;

/// <summary>
/// Represents a read-only repository for accessing student data.
/// </summary>
/// <remarks>This interface provides methods to retrieve student information without modifying the underlying
/// data. Implementations of this interface are expected to handle data retrieval operations, such as querying a
/// database or an external service.</remarks>
public interface IReadOnlyRepo
{
    /// <summary>
    /// Retrieves a collection of students asynchronously.
    /// </summary>
    /// <remarks>The returned collection contains all students currently available in the system. If no
    /// students are found, the collection will be empty.</remarks>
    /// <returns>A task representing the asynchronous operation. The task result contains an 
    /// <see cref="IEnumerable{Student}"/> of students.</returns>
    public Task<IEnumerable<Student>> GetStudents();

    /// <summary>
    /// Retrieves a student by their unique identifier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the student data. Ensure that the
    /// <paramref name="id"/> provided is valid and corresponds to an existing student in the system.</remarks>
    /// <param name="id">The unique identifier of the student to retrieve. Must be a positive integer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Student"/> object 
    /// corresponding to the specified identifier.</returns>
    public Task<Student> GetStudentById(int id);
}

/// <summary>
/// Provides read-only access to student data.
/// </summary>
/// <remarks>This class implements the <see cref="IReadOnlyRepo"/> interface and provides methods to retrieve
/// student information. All operations are asynchronous and simulate data retrieval with a delay.</remarks>
public class ReadOnlyRepo : IReadOnlyRepo
{
    /// <summary>
    /// Asynchronously retrieves a collection of students.
    /// </summary>
    /// <remarks>The method simulates a delay to mimic an asynchronous operation, such as fetching data from a
    /// remote source. The returned collection contains predefined student objects with their respective IDs, names, and
    /// subjects.</remarks>
    /// <returns>A task representing the asynchronous operation. When completed, the task returns an <see
    /// cref="IEnumerable{Student}"/>  containing the list of students.</returns>
    public async Task<IEnumerable<Student>> GetStudents()
    {
        await Task.Delay(1000);

        var students = new List<Student>()
        {
            new(1, "S1", "Sub1"),
            new(2, "S2", "Sub2"),
            new(3, "S3", "Sub3"),
            new(4, "S4", "Sub4")
        };

        return students;
    }

    /// <summary>
    /// Retrieves a student by their unique identifier.
    /// </summary>
    /// <remarks>This method simulates an asynchronous operation and includes a delay. The returned <see
    /// cref="Student"/> object contains the provided identifier and placeholder data for demonstration
    /// purposes.</remarks>
    /// <param name="id">The unique identifier of the student to retrieve. Must be a positive integer.</param>
    /// <returns>A <see cref="Student"/> object representing the student with the specified identifier.</returns>
    public async Task<Student> GetStudentById(int id)
    {
        await Task.Delay(1000);
        var student = new Student(id, $"{id} s3", "Sub3");
        return student;
    }
}
