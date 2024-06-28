using OfficeManager.Domain.DataObjects.Offices.Requests;
using OfficeManager.Domain.Entities.EntityBases;
using OfficeManager.Domain.Validators;

namespace OfficeManager.Domain.Entities
{
    public class Office : EntityBase
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public List<User> Users { get; private set; }

        public static Office BuildWith(OfficeRequest request)
        {
            return new Office
            {
                Cnpj = request.Cnpj,
                Name = request.Name,
            };
        }

        public void Update(Office office)
        {
            Name = office.Name;
            Cnpj = office.Cnpj;
            Users = office.Users;
        }

        public override bool IsValid()
        {
            var validator = new OfficeValidator();
            var result = validator.Validate(this);

            AddErrors(result.Errors.Select(x => x.ErrorMessage).ToArray());

            return Errors.Count == 0;
        }
    }
}
