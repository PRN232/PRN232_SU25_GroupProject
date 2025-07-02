using System;
using System.Linq;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.Presentation.Initialization
{
    public static class MedicalHistoryAndVisionHearingSeeder
    {
        public static void SeedMedicalHistories(SchoolMedicalDbContext context)
        {
            if (context.MedicalHistories.Any()) return;

            var medicalprofile = context.MedicalProfiles.ToList();

            context.MedicalHistories.AddRange(
                new MedicalHistory
                {
                    MedicalProfileId = medicalprofile[0].Id,
                    Condition = "Sốt xuất huyết",
                    Treatment = "Nghỉ ngơi, uống nhiều nước, hạ sốt bằng Paracetamol",
                    TreatmentDate = new DateTime(2023, 8, 15),
                    Doctor = "BS Trần Văn A",
                    Notes = "Đã nhập viện 5 ngày tại BV Nhi Đồng 1"
                },
                new MedicalHistory
                {
                    MedicalProfileId = medicalprofile[2].Id,
                    Condition = "Viêm phế quản",
                    Treatment = "Kháng sinh Amoxicillin 250mg",
                    TreatmentDate = new DateTime(2022, 11, 3),
                    Doctor = "BS Nguyễn Văn B",
                    Notes = "Tái phát 2 lần trong năm học"
                },
                new MedicalHistory
                {
                    MedicalProfileId = medicalprofile[4].Id,
                    Condition = "Suyễn nhẹ",
                    Treatment = "Sử dụng thuốc xịt hen khi cần thiết",
                    TreatmentDate = new DateTime(2021, 3, 20),
                    Doctor = "BS Trần Văn A",
                    Notes = "Không cần dùng thuốc thường xuyên"
                }
            );

            context.SaveChanges();
        }

        public static void SeedVisionHearing(SchoolMedicalDbContext context)
        {
            if (context.VisionHearings.Any()) return;

            var medicalprofile = context.MedicalProfiles.ToList();

            context.VisionHearings.AddRange(
                new VisionHearing
                {
                    MedicalProfileId = medicalprofile[0].Id,
                    VisionLeft = "10/10",
                    VisionRight = "10/10",
                    HearingLeft = "Bình thường",
                    HearingRight = "Bình thường",
                    LastChecked = DateTime.Today.AddMonths(-1)
                },
                new VisionHearing
                {
                    MedicalProfileId = medicalprofile[1].Id,
                    VisionLeft = "8/10",
                    VisionRight = "9/10",
                    HearingLeft = "Bình thường",
                    HearingRight = "Bình thường",
                    LastChecked = DateTime.Today.AddMonths(-1)
                },
                new VisionHearing
                {
                    MedicalProfileId = medicalprofile[3].Id,
                    VisionLeft = "10/10",
                    VisionRight = "9/10",
                    HearingLeft = "Nghe kém nhẹ",
                    HearingRight = "Bình thường",
                    LastChecked = DateTime.Today.AddMonths(-2)
                }
            );

            context.SaveChanges();
        }
    }
}