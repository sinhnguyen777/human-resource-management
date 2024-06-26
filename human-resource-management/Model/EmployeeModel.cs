using System;
using System.Collections.Generic;
using human_resource_management.repositories;

namespace human_resource_management.Model
{
    public enum GenderEnum
    {
        Male,
        Female,
        Other
    }

    public class EmployeeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthday { get; set; }
        public GenderEnum Sex { get; set; }
        public string? Salary { get; set; }
        public string? Position { get; set; }
        public int? IdDepartment { get; set; }
    }

    public class EmployeeRepository : BaseRepository.Repository<EmployeeModel>
    {
        private List<EmployeeModel> employees = new List<EmployeeModel>();
        private int nextId = 1;

        public override void Add(EmployeeModel entity)
        {
            entity.Id = nextId++;
            employees.Add(entity);
        }

        public override void Update(EmployeeModel entity)
        {
            EmployeeModel? existingEmployee = employees.Find(item => item.Id == entity.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = entity.Name;
                existingEmployee.Birthday = entity.Birthday;
                existingEmployee.Sex = entity.Sex;
                existingEmployee.Salary = entity.Salary;
                existingEmployee.Position = entity.Position;
                existingEmployee.IdDepartment = entity.IdDepartment;
            }
            else
            {
                throw new ArgumentException("Employee not found");
            }
        }

        public override void Delete(EmployeeModel entity)
        {
            EmployeeModel? existingEmployee = employees.Find(item => item.Id == entity.Id);
            if (existingEmployee != null)
            {
                employees.Remove(existingEmployee);
            }
            else
            {
                throw new ArgumentException("Employee not found");
            }
        }

        public override EmployeeModel GetById(int id)
        {
            EmployeeModel? existingEmployee = employees.Find(item => item.Id == id);
            return existingEmployee;
        }

        public override List<EmployeeModel> GetAll()
        {
            return employees;
        }

        public void ClearAll()
        {
            employees.Clear();
            nextId = 1;
        }

    }
}