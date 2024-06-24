namespace Resume_Builder.Domain.Entities.Resume;

public class Resumes:BaseEntity
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public string SocialMediaUserName { get; set; }
    public string Rule { get; set; }
    public string Experience { get; set; }
    public int Salary { get; set; }
    public string WorkingType { get; set; }
    public string Skills { get; set; }
    public string Languages { get; set; }
    public string Projects { get; set; }
    public string Photo { get; set; }
    public string Portfolio { get; set; }
    public string Github { get; set; }
    public string Linkedin { get; set; }
    public DateTime CreatedAt { get; set; }
}

