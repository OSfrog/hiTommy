﻿using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.ViewModels;
using hiTommy.Models;

namespace hiTommy.Data.Services
{
    public class ShoeServices
    {
        private readonly HiTommyApplicationDbContext _context;

        public ShoeServices(HiTommyApplicationDbContext context)
        {
            _context = context;
        }

        public void AddShoe(ShoeViewModel shoe)
        {
            var _shoe = new Shoe
            {
                Name = shoe.Name,
                Price = shoe.Price,
                BrandId = shoe.BrandId,
                IsOnSale = false,
                SalePrice = null,
                Description = shoe.Description,
                PictureUrl = shoe.PictureUrl,

                Brand = _context.Brands.Where(n => n.Id == shoe.BrandId).Select(n => n.Name).FirstOrDefault()
            };
            _context.Shoes.Add(_shoe);
            _context.SaveChanges();
        }

        public List<Shoe> GetAllShoes()
        {
            return _context.Shoes.ToList();
        }

        public Shoe GetShoeById(int shoeId)
        {
            return _context.Shoes.FirstOrDefault(n => n.Id == shoeId);
        }

        public Shoe UpdateShoeById(int shoeId, ShoeViewModel shoe)
        {
            var _shoe = _context.Shoes.FirstOrDefault(n => n.Id == shoeId);
            if (_shoe is null) return _shoe;
            _shoe.Name = shoe.Name;
            _shoe.Price = shoe.Price;
            _shoe.IsOnSale = false;
            _shoe.SalePrice = null;
            _shoe.BrandId = shoe.BrandId;

            _context.SaveChanges();

            return _shoe;
        }

        public Shoe SetShoeOnSaleById(int shoeId, ShoeSaleViewModel shoe)
        {
            var _shoe = _context.Shoes.FirstOrDefault(n => n.Id == shoeId);
            if (_shoe is null) return _shoe;
            _shoe.SalePrice = shoe.IsOnSale ? shoe.SalePrice : null;

            _context.SaveChanges();

            return _shoe;
        }

        public void DeleteShoeById(int shoeId)
        {
            var _shoe = _context.Shoes.FirstOrDefault(n => n.Id == shoeId);
            if (_shoe is null) return;
            _context.Shoes.Remove(_shoe);
            _context.SaveChanges();
        }
    }
}