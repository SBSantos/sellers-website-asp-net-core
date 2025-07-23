using SalesWebApp.Models;

namespace SalesWebApp.Data
{
    public class SeedingService
    {
        private readonly SalesWebAppContext _context;

        public SeedingService(SalesWebAppContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any()) { return; } // DB has been seeded

            Department d1 = new Department(id: 1, name: "Computers");
            Department d2 = new Department(id: 2, name: "Electronics");
            Department d3 = new Department(id: 3, name: "Fashion");
            Department d4 = new Department(id: 4, name: "Books");

            Seller s1 = new Seller(
                id: 1, 
                name: "SellerOne", 
                email: "SellerOne@gmail.com", 
                baseSalary: 1000.0, 
                birthDate: new DateTime(1998, 4, 21), 
                department: d1
                );

            Seller s2 = new Seller(
                id: 2,
                name: "SellerTwo",
                email: "SellerTwo@gmail.com",
                baseSalary: 1000.0,
                birthDate: new DateTime(2000, 5, 18),
                department: d2
                );

            Seller s3 = new Seller(
                id: 3,
                name: "SellerThree",
                email: "SellerThree@gmail.com",
                baseSalary: 1000.0,
                birthDate: new DateTime(1999, 2, 12),
                department: d3
                );

            Seller s4 = new Seller(
                id: 4,
                name: "SellerFour",
                email: "SellerFour@gmail.com",
                baseSalary: 1000.0,
                birthDate: new DateTime(2002, 7, 4),
                department: d4
                );

            SalesRecord sr1 = new SalesRecord(
                id: 1,
                date: new DateTime(2025, 7, 22),
                amount: 11000.0,
                status: Models.Enums.SaleStatus.Billed,
                seller: s1
                );

            SalesRecord sr2 = new SalesRecord(
                id: 2,
                date: new DateTime(2025, 3, 1),
                amount: 8000.0,
                status: Models.Enums.SaleStatus.Cancelled,
                seller: s2
                );

            SalesRecord sr3 = new SalesRecord(
                id: 3,
                date: new DateTime(2025, 8, 11),
                amount: 3000.0,
                status: Models.Enums.SaleStatus.Pending,
                seller: s3
                );

            SalesRecord sr4 = new SalesRecord(
                id: 4,
                date: new DateTime(2025, 2, 6),
                amount: 300.0,
                status: Models.Enums.SaleStatus.Billed,
                seller: s4
                );

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecord.AddRange(sr1, sr2, sr3, sr4);
            _context.SaveChanges();
        }
    }
}
