using src.Enums;

namespace src.ViewModels
{
    public class StudentViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string MatricNumber { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty; 
        public Level? Level { get; set; }
        public string DisplayLevel => Level?.GetDisplayName() ?? "Not specified";
    }
}
