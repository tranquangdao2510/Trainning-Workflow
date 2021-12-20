using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.UI;
using MultipleDbContextDemo.Categories.Dtos;
using MultipleDbContextDemo.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleDbContextDemo.Categories
{
    internal class CategoryAppService : MultipleDbContextDemoAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category> _repository;
        private readonly IDbContextProvider<MySecondDbContext> _mySecondDbContext;

        public CategoryAppService(IRepository<Category> repository, IDbContextProvider<MySecondDbContext> mySecondDbContext)
        {
            _repository = repository;
            _mySecondDbContext = mySecondDbContext;
        }

        public int Create(CreateCategory input)
        {
            try
            {
                var cate = new Category()
                {
                    Id = input.Id,
                    Name = input.Name,
                    Active = input.Active
                };
                _repository.Insert(cate);
                return cate.Id;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = _repository.GetAll().Where(x => x.Id == id).FirstOrDefault();
                if (cate != null)
                {
                    _repository.Delete(cate);
                    return true;
                }
                else
                {
                    throw new UserFriendlyException("delete does not success");
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public CategoryDto Detail(int id)
        {
            try
            {
                CategoryDto cateDto = new CategoryDto();
                if (id != 0)
                {
                    var cate = _repository.GetAll().Where(x => x.Id == id).Select(y => new CategoryDto
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Active = y.Active
                    }).FirstOrDefault();
                    cateDto.Id = cate.Id;
                    cateDto.Name = cate.Name;
                    cateDto.Active = cate.Active;
                }
                return cateDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public int Edit(UpdateCategory input)
        {
            try
            {
                var cate = _repository.GetAll().Where(x => x.Id == input.Id).FirstOrDefault();
                if (cate != null)
                {
                    cate.Name = input.Name;
                    cate.Active = input.Active;
                    _repository.UpdateAsync(cate);
                    return cate.Id;
                }
                else
                {
                    throw new UserFriendlyException("Update does not success");
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public List<Category> GetCategories()
        {
            try
            {
                //ListResultDto<Category> lstcate = new ListResultDto<Category>();
                var lstcate = _repository.GetAllList().Select(x => new Category
                {
                    Id = x.Id,
                    Name = x.Name,
                    Active = x.Active
                }).ToList();
                return lstcate;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public List<Category> GetCategoriesLinq()
        {
            try
            {
                var cate = (from c in _mySecondDbContext.GetDbContext().Categories
                            select c).ToList();

                return cate;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public void CreateLinq(CreateCategory input)
        {
            try
            {
                Category cate = new Category();
                cate.Id = input.Id;
                cate.Name = input.Name;
                cate.Active = input.Active;

                var c = _mySecondDbContext.GetDbContext().Categories.Add(cate);

                _repository.InsertAsync(c);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public void EditLinq(UpdateCategory input)
        {
            try
            {
                var cate = (from c in _mySecondDbContext.GetDbContext().Categories
                            where c.Id == input.Id
                            select c).FirstOrDefault();
                if (cate.Id != 0)
                {
                    cate.Name = input.Name;
                    cate.Active = input.Active;
                    _repository.Update(cate);
                }
                else
                {
                    throw new UserFriendlyException("Update does not success");
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public bool DeleteLinq(int id)
        {
            try
            {
                var cate = (from c in _mySecondDbContext.GetDbContext().Categories
                            where c.Id == id
                            select c).FirstOrDefault();
                if (cate.Id != 0)
                {
                    _repository.Delete(cate);

                    return true;
                }
                else
                {
                    throw new UserFriendlyException("delete does not success");
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public CategoryDto DetailLinq(int id)
        {
            try
            {
                CategoryDto cateDto = new CategoryDto();
                var cate = (from c in _mySecondDbContext.GetDbContext().Categories
                            where c.Id == id
                            select new CategoryDto()
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Active = c.Active
                            }).First();

                cateDto.Id = cate.Id;
                cateDto.Name = cate.Name;
                cateDto.Active = cate.Active;

                return cateDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        // sql thuan

        public List<Category> GetCategorySql()
        {
            try
            {
                var sqlCate = "select * from Category";
                var cate = _mySecondDbContext.GetDbContext().Database.SqlQuery<Category>(sqlCate).ToList();
                return cate;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public int CreateSql(CreateCategory input)
        {
            try
            {
                Category cate = new Category();
                cate.Name = input.Name;
                cate.Active = input.Active;

                var sqlCate = "insert into Category(Name,Active) values(@Name,@Active)";
                var name = new SqlParameter("@Name", cate.Name);
                var active = new SqlParameter("@Active", cate.Active);
                var c = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sqlCate, name, active);
                return c;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public int EditSql(UpdateCategory input)
        {
            try
            {
                var getIdcate = "select Id,Name,Active from Category where Id=@id";
                var cid = new SqlParameter("@id", input.Id);
                var cate = _mySecondDbContext.GetDbContext().Database.SqlQuery<Category>(getIdcate, cid).FirstOrDefault();

                var sqlCate = "update Category set Name=@name,Active=@active where Id=@id ";
                var cateid = new SqlParameter("id", cate.Id);
                var name = new SqlParameter("name", input.Name);
                var active = new SqlParameter("active", input.Active);
                var c = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sqlCate, name, active, cateid);
                return c;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public bool DeleteSql(int id)
        {
            try
            {
                var getIdcate = "select Id,Name,Active from Category where Id=@id";
                var cid = new SqlParameter("@id", id);
                var cate = _mySecondDbContext.GetDbContext().Database.SqlQuery<Category>(getIdcate, cid).FirstOrDefault();
                if (cate.Id != 0)
                {
                    var sqlCate = "delete Category where Id=@id";
                    var cateId = new SqlParameter("id", cate.Id);
                    var c = _mySecondDbContext.GetDbContext().Database.ExecuteSqlCommand(sqlCate, cateId);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }

        public CategoryDto DetailSql(int id)
        {
            try
            {
                var getIdcate = "select Id,Name,Active from Category where Id=@id";
                var cid = new SqlParameter("@id", id);
                var cate = _mySecondDbContext.GetDbContext().Database.SqlQuery<Category>(getIdcate, cid).FirstOrDefault();

                CategoryDto cateDto = new CategoryDto();
                cateDto.Id = cate.Id;
                cateDto.Name = cate.Name;
                cateDto.Active = cate.Active;
                return cateDto;
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(e.Message);
            }
        }
    }
}