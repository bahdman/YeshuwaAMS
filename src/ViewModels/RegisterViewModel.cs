using src.Enums;

namespace src.ViewModels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Program { get; set; }
        public Level Level { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string EntryMode { get; set; }
    }
}
