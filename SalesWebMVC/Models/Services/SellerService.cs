﻿using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;  /*para incluir */
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        //public List<Seller> FindAll()
        //{
        //    return _context.Seller.ToList();

        //}

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();

        }


        //public void Insert(Seller obj)
        //{
        //    //obj.Department = _context.Department.First();
        //    _context.Seller.Add(obj);
        //    _context.SaveChanges();
        //}

        public async Task InsertAsync(Seller obj)
        {
            //obj.Department = _context.Department.First();
            _context.Seller.Add(obj);
            await _context.SaveChangesAsync();
        }


        //public Seller FindById(int id)
        //{

        //    //return _context.Seller.FirstOrDefault(obj => obj.Id == id);   /*Join das Tabelas*/
        //    return _context.Seller.Include(obj =>obj.Department).FirstOrDefault(obj => obj.Id == id);

        //}

        public async Task<Seller> FindByIdAsync(int id)
        {

            //return _context.Seller.FirstOrDefault(obj => obj.Id == id);   /*Join das Tabelas*/
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

        }

        //public void Remove(int id)
        //{
        //    var obj = _context.Seller.Find(id);
        //    _context.Seller.Remove(obj);
        //    _context.SaveChanges();
        //}

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
                //Se quiser personalizar
                //throw new IntegrityException("Can't delete seller because h/she has Sales");
            }

        }

        //public void Update(Seller obj)
        //{
        //    if(!_context.Seller.Any(x=>x.Id==obj.Id))
        //    {
        //        throw new NotFoundException("Id not found");
        //    }
        //    try
        //    {
        //        _context.Update(obj);
        //        _context.SaveChanges();
        //    }
        //    catch (DBConcurrencyException e)
        //    {

        //        throw new DBConcurrencyException(e.Message);
        //    }

            
        //}

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {

                throw new DBConcurrencyException(e.Message);
            }


        }
    }
}
