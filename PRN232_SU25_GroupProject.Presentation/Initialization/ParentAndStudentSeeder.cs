using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class ParentAndStudentSeeder
    {
        public static void SeedParents(SchoolMedicalDbContext context)
        {
            if (context.Parents.Any()) return;

            var mapping = new (string UserName, string FullName, string Phone, string Addr)[]
            {
                ("parent001", "Nguyễn Thị Lan", "0909111001", "12 Nguyễn Huệ, Q1"),
                ("parent002", "Phạm Văn Long", "0909111002", "45 Pasteur, Q3"),
                ("parent003", "Trần Thị Hòa", "0909111003", "67 Điện Biên Phủ, Q10"),
                ("parent004", "Lê Văn Bình", "0909111004", "89 CMT8, Tân Bình"),
                ("parent005", "Võ Thị Mai", "0909111005", "23A Hoàng Hoa Thám, Phú Nhuận"),
                ("parent006", "Bùi Anh Dũng", "0909111006", "78 Hoàng Sa, Bình Thạnh"),
                ("parent007", "Trịnh Hữu Ngọc", "0909111007", "100A Tô Hiến Thành, Q10")
            };
            foreach (var (userName, fullName, phone, addr) in mapping)
            {
                var user = context.Users.First(u => u.UserName == userName);
                context.Parents.Add(new Parent { UserId = user.Id, FullName = fullName, PhoneNumber = phone, Address = addr });
            }
            context.SaveChanges();
        }

        public static void SeedStudents(SchoolMedicalDbContext context)
        {
            if (context.Students.Any()) return;

            var parents = context.Parents.ToDictionary(p => p.FullName);

            context.Students.AddRange(
                new Student { StudentCode = "STU001", FullName = "Nguyễn Minh Khang", DateOfBirth = new DateTime(2012, 4, 3), Gender = "Nam", ClassName = "5A1", ParentId = parents["Nguyễn Thị Lan"].Id },
                new Student { StudentCode = "STU002", FullName = "Nguyễn Minh Khuê", DateOfBirth = new DateTime(2014, 7, 15), Gender = "Nữ", ClassName = "3B2", ParentId = parents["Nguyễn Thị Lan"].Id },
                new Student { StudentCode = "STU003", FullName = "Phạm Mai Chi", DateOfBirth = new DateTime(2013, 1, 20), Gender = "Nữ", ClassName = "4C1", ParentId = parents["Phạm Văn Long"].Id },
                new Student { StudentCode = "STU004", FullName = "Phạm Gia Huy", DateOfBirth = new DateTime(2011, 11, 2), Gender = "Nam", ClassName = "6A1", ParentId = parents["Phạm Văn Long"].Id },
                new Student { StudentCode = "STU005", FullName = "Trần Bảo Khánh", DateOfBirth = new DateTime(2015, 9, 5), Gender = "Nữ", ClassName = "2A4", ParentId = parents["Trần Thị Hòa"].Id },
                new Student { StudentCode = "STU006", FullName = "Lê Gia Bảo", DateOfBirth = new DateTime(2010, 12, 12), Gender = "Nam", ClassName = "7B3", ParentId = parents["Lê Văn Bình"].Id },
                new Student { StudentCode = "STU007", FullName = "Võ Hoàng Anh", DateOfBirth = new DateTime(2013, 5, 28), Gender = "Nam", ClassName = "4A3", ParentId = parents["Võ Thị Mai"].Id },
                new Student { StudentCode = "STU008", FullName = "Bùi Thị Thảo", DateOfBirth = new DateTime(2014, 3, 12), Gender = "Nữ", ClassName = "3C1", ParentId = parents["Bùi Anh Dũng"].Id },
                new Student { StudentCode = "STU009", FullName = "Trịnh Minh Phát", DateOfBirth = new DateTime(2015, 6, 2), Gender = "Nam", ClassName = "2B3", ParentId = parents["Trịnh Hữu Ngọc"].Id }
            );
            context.SaveChanges();
        }
    }
}