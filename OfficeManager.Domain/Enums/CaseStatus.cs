namespace OfficeManager.Domain.Enums
{
    public enum CaseStatus
    {
        New = 1,
        InReview = 2,
        WaitingForClient = 3,
        WaitingForCourt = 4,
        InProgress = 5,
        Resolved = 6,
        Closed = 7,
        Reopened = 8,
        OnHold = 9
    }
}
