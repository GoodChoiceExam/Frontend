namespace FitLife.Frontend.Models;

public class WorkoutProgram
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Exercise> Exercises { get; set; } = [];
    public int ExerciseCount => Exercises.Count;
}

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public decimal? WeightKg { get; set; }
}