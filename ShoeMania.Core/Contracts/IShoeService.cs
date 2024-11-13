﻿using ShoeMania.Core.ViewModels.Shoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeMania.Core.Contracts
{
    public interface IShoeService
    {
        Task<AllShoesFilteredAndPaged> GetAllShoesFilteredAndPagedAsync(ShoesQueryModel model);

        Task<string> AddAsync(ShoeFormModel model);

        Task<bool> IsExistsAsync(string id);


       
    }
}