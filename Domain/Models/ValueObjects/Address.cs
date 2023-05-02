namespace Domain.Models.ValueObjects
{
    public class Address
    {
        public string State { get; private set;}
        public string City { get; private set;}
        public string District { get; private set;}
        public string Street { get; private set;}
        public int StreetNumber { get; private set;}
        public string Cep { get; private set;}
        public string? Complement { get; private set;}

        public Address(
            string state, 
            string city, 
            string district, 
            string street, 
            int streetNumber, 
            string cep, 
            string? complement = null)
        {
            State = state;
            City = city;
            District = district;
            Street = street;
            StreetNumber = streetNumber;
            Cep = cep;
            Complement = complement;
        }
    }
}