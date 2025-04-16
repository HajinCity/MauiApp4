namespace MauiApp4.Models
{
    public class Complaint
    {
        public string? CaseID { get; set; }
        public ComplainantDetails? Complainant { get; set; }
        public RespondentDetails? Respondent { get; set; }
        public CaseDetails? CaseDetails { get; set; }
    }

    public class ComplainantDetails
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? SexIdentification { get; set; }
        public string? CivilStatus { get; set; }
        public string? Birthdate { get; set; }
        public string? Age { get; set; }
        public string? Religion { get; set; }
        public string? CellNumber { get; set; }
        public string? Nationality { get; set; }
        public string? Occupation { get; set; }
        public Address? Address { get; set; }
    }

    public class RespondentDetails
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Alias { get; set; }
        public string? SexIdentification { get; set; }
        public string? CivilStatus { get; set; }
        public string? Birthdate { get; set; }
        public string? Age { get; set; }
        public string? Religion { get; set; }
        public string? CellNumber { get; set; }
        public string? Nationality { get; set; }
        public string? Occupation { get; set; }
        public string? RelationshipToComplainant { get; set; }
        public Address? Address { get; set; }
    }

    public class CaseDetails
    {
        public string? ComplaintDate { get; set; }
        public string? VAWCCase { get; set; }
        public string? SubCase { get; set; }
        public string? CaseStatus { get; set; }
        public string? ReferredTo { get; set; }
        public string? IncidentDate { get; set; }
        public string? IncidentDescription { get; set; }
        public string? CaseNumber { get; set; }
        public Address? PlaceOfIncident { get; set; }
    }

    public class Address
    {
        public string? Purok { get; set; }
        public string? Barangay { get; set; }
        public string? Municipality { get; set; }
        public string? Province { get; set; }
        public string? Region { get; set; }
    }
}
