namespace OfficeManager.Domain.Entities.EntityBases
{
    public abstract class ErrorBase
    {
        private readonly List<string> _errors;

        public IReadOnlyCollection<string> Errors => _errors;

        protected ErrorBase()
        {
            _errors = new List<string>();
        }

        protected void AddError(string error)
        {
            _errors.Add(error);
        }

        public void AddErrors(params string[] errors)
        {
            if (errors != null)
            {
                _errors.AddRange(errors);
            }
        }

        public void ClearErrors()
        {
            _errors.Clear();
        }

        public virtual bool IsValid()
        {
            return !_errors.Any();
        }
    }
}
