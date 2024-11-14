﻿using Core.Entities;
using Core.Specificatioon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams) : base(x =>
             (string.IsNullOrEmpty(specParams.Search)||x.Name.ToLower().Contains(specParams.Search))&&
             (specParams.Brands.Count() ==0|| specParams.Brands.Contains(x.Brand)) &&
             (specParams.Types.Count()==0|| specParams.Types.Contains(x.Type))
        )


        {

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize); //(skip,take)
            switch (specParams.Sort)
            {
                case "PriceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "PriceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;

            }

        }




    }
}
