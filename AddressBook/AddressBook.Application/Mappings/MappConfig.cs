using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.AccountDTOs;
using AddressBook.Presentation.DTOs.AddressBookDTOs;
using AddressBook.Presentation.DTOs.DepartmentDTOs;
using AddressBook.Presentation.DTOs.JobDTOs;
using AutoMapper;

namespace AddressBook.Presentation.Mappings
{
    public class MappConfig:Profile
    {
        public MappConfig()
        {
            // -------------- Department Mappings --------------
            //Read
            CreateMap<Department, ReadDepartmentDTO>().ReverseMap();
            //Add
            CreateMap<Department, AddDepartmentDTO>().ReverseMap();
            //Update
            CreateMap<Department, UpdateDepartmentDTO>().ReverseMap();
            //-------------------------- Job Mappings ----------------------------
            //Read
            CreateMap<Job, ReadJobsDTO>().AfterMap((src, dest) =>
            {
                dest.JobId = src.Id;
                dest.JobTitle = src.Title;
                dest.JobDescription = src.Description;
            }).ReverseMap();
            //Add
            CreateMap<Job, AddJobDTO>().ReverseMap();
            CreateMap<Job, UpdateJobDTO>().ReverseMap();
            //------------------ AddressBookEntry Mappings ------------------
            CreateMap<AddressBookEntry, ReadAddressBookDTO>().AfterMap((src, dest) =>
            {
                dest.AddressBookId = src.Id;
                dest.JobTitle = src?.Job?.Title;
                dest.DepartmentName = src?.Department?.Name;
                dest.Age = src?.Age??0;
            }).ReverseMap();
            CreateMap<AddressBookEntry, AddAddressBookDTO>().ReverseMap();
            CreateMap<AddressBookEntry, UpdateAddressBookDTO>().ReverseMap();
            //--------------------- Account Mappings -----------------
            CreateMap<RegisterDTO, ApplicationUser>().AfterMap((src, dest) =>
            {
                dest.UserName = src.UserName;
                dest.Email = src.Email;
            }).ReverseMap();
        }
    }
}
