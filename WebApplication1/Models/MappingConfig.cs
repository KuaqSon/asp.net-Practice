using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;


namespace WebApplication1.Models
{
    public class MappingConfig
    {
        public static void RegisterMapping()
        {
            AutoMapper.Mapper.Initialize(config => {
                config.CreateMap<Customer, Employee>();
            });
            //var mapperConfig = new MapperConfiguration(config => {
            //    config.CreateMap<Customer, Employee>();
            //});
        }
    }
}