﻿using SalesWebApp.Data;
using SalesWebApp.Models;
using SalesWebApp.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SalesWebApp.Services
{
    public class SellerService
    {
        private readonly SalesWebAppContext _context;

        public SellerService(SalesWebAppContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task Insert(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller?> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            bool saleNull = await _context.Seller.AllAsync(x => x.Sales == null);
            if (!saleNull)
            {
                throw new IntegrityException("Not possible to remove seller who has registered sales");
            }

            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny) { throw new NotFoundException("Id not found."); }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
