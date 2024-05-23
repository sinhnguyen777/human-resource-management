using System.Collections.Generic;
using human_resource_management.repositories;

namespace human_resource_management.Model
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeamSize { get; set; }
        public List<int>? ListEmployees { get; set; }
        public int? IdManager { get; set; }
    }

    public class DepartmentRepository : BaseRepository.Repository<DepartmentModel>
    {
        private List<DepartmentModel> _department = new List<DepartmentModel>();
        private int nextId = 1;

        public override List<DepartmentModel> GetAll()
        {
            return _department;
        }

        public override DepartmentModel GetById(int id)
        {
            DepartmentModel? existingDepartment = _department.Find(item => item.Id == id);
            if (existingDepartment != null)
            {
                return existingDepartment;
            }
            else
            {
                throw new ArgumentException("Department not found");
            }
        }

        public override void Add(DepartmentModel entity)
        {
            entity.Id = nextId++;
            _department.Add(entity);
        }

        public override void Update(DepartmentModel entity)
        {
            DepartmentModel? existingDepartment = _department.Find(item => item.Id == entity.Id);

            if (existingDepartment != null)
            {
                existingDepartment.Name = entity.Name;
                existingDepartment.TeamSize = entity.TeamSize;
                existingDepartment.IdManager = entity.IdManager;
                existingDepartment.ListEmployees = entity.ListEmployees;
            }
            else
            {
                throw new ArgumentException("Department not found");
            }
        }

        public override void Delete(DepartmentModel entity)
        {
            DepartmentModel? existingDepartment = _department.Find(item => item.Id == entity.Id);

            if (existingDepartment != null)
            {
                _department.Remove(existingDepartment);
            }
            else
            {
                throw new ArgumentException("Department not found");
            }
        }

        public string GetDepartmentNameById(int? id)
        {
            if (id == null)
                return "Không xác định";

            var department = GetAll().FirstOrDefault(d => d.Id == id);
            return department?.Name ?? "Không xác định";
        }
    }
}