using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APIWebMovie.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieWebContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(MovieWebContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TMapper> GetById<TMapper>(int id) where TMapper : class
        {
            var entity = await _context.Set<T>().FindAsync(id);
            var entityView = _mapper.Map<TMapper>(entity);
            return entityView;
        }

        public async Task<IEnumerable<TMapper>> GetAll<TMapper>() where TMapper : class
        {
            var listEntity = await _context.Set<T>().ToListAsync();
            var listEntityView = _mapper.Map<List<TMapper>>(listEntity);
            return listEntityView;
        }

        public async Task<bool> Add<TMapper>(TMapper mapper) where TMapper : class
        {
            try
            {
                var entity = _mapper.Map<T>(mapper);
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<List<TMapper>> FindToList<TMapper>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, object>> orderBy = null, int? quantity = null) where TMapper : class
        {
            IQueryable<T> entity = _context.Set<T>();
            if(predicate != null)
            {
                entity = entity.Where(predicate);
            }
            if (orderBy != null)
            {
                entity = entity.OrderByDescending(orderBy);
            }
            if(quantity != null)
            {
                entity = entity.Take(quantity.Value);
            }
            var entityView = await _mapper.ProjectTo<TMapper>(entity).ToListAsync();
            return entityView;
        }

        public async Task<TMapper> Find<TMapper>(Expression<Func<T, bool>> predicate) where TMapper : class
        {
            var entity = await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
            var entityView = _mapper.Map<TMapper>(entity);
            return entityView;
        }

        public async Task<T> FindToEntity(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
            return entity;
        }
    }
}
