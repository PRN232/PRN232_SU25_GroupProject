﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<StudentDto>> CreateStudentAsync(CreateStudentRequest request)
        {

            var student = _mapper.Map<Student>(request);
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();


            var medicalProfile = new MedicalProfile
            {
                StudentId = student.Id,
                LastUpdated = DateTime.UtcNow,

            };
            await _unitOfWork.GetRepository<MedicalProfile>().AddAsync(medicalProfile);
            await _unitOfWork.SaveChangesAsync();


            var studentWithRelations = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .FirstOrDefaultAsync(s => s.Id == student.Id);

            if (studentWithRelations == null)
                return ApiResponse<StudentDto>.ErrorResult("Tạo học sinh thất bại.");

            return ApiResponse<StudentDto>.SuccessResult(_mapper.Map<StudentDto>(studentWithRelations), "Tạo học sinh thành công.");
        }

        public async Task<ApiResponse<List<StudentDto>>> GetAllStudentsAsync()
        {
            List<Student> students = (await _unitOfWork.StudentRepository.GetAllAsync()).ToList();
            return ApiResponse<List<StudentDto>>.SuccessResult(_mapper.Map<List<StudentDto>>(students));
        }

        public async Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return ApiResponse<StudentDto>.ErrorResult("Không tìm thấy học sinh.");
            return ApiResponse<StudentDto>.SuccessResult(_mapper.Map<StudentDto>(student));
        }

        public async Task<ApiResponse<List<StudentDto>>> GetStudentsByClassAsync(string className)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .Where(s => s.ClassName == className)
                .ToListAsync();

            return ApiResponse<List<StudentDto>>.SuccessResult(_mapper.Map<List<StudentDto>>(students));
        }

        public async Task<ApiResponse<List<StudentDto>>> GetStudentsByParentAsync(int parentId)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
                .Include(s => s.MedicalProfile)
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            return ApiResponse<List<StudentDto>>.SuccessResult(_mapper.Map<List<StudentDto>>(students));
        }

        public async Task<ApiResponse<bool>> UpdateStudentAsync(int id, UpdateStudentRequest request)
        {
            var student = await _unitOfWork.StudentRepository
                .Query()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy học sinh để cập nhật.");

            _mapper.Map(request, student);
            _unitOfWork.StudentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResult(true, "Cập nhật học sinh thành công.");
        }
        public async Task<ApiResponse<List<ClassSummaryDto>>> GetClassSummariesAsync()
        {
            var groups = await _unitOfWork.StudentRepository.Query()
                .GroupBy(s => s.ClassName)
                .Select(g => new
                {
                    ClassName = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var dtos = groups
                .Select(g => new ClassSummaryDto
                {
                    ClassName = g.ClassName,
                    StudentCount = g.Count
                })
                .ToList();

            return ApiResponse<List<ClassSummaryDto>>.SuccessResult(dtos);
        }
    }
}
