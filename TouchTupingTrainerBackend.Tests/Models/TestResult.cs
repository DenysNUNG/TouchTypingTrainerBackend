using TouchTypingTrainerBackend.Models;

namespace TouchTupingTrainerBackend.Tests.Models
{
    public class TestResult : IUserResult
    {
        public float Accuracy { get; set; }
        public int Speed { get; set; }
    }
}
