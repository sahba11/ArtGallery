//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Ticketing.Model.Entities.MenuItemEntity;
//using Ticketing.Models.Entities.ResourceEntity;
//using Ticketing.Models.Entities.RoleEntity;
//using Ticketing.Models.Entities.TicketEntity;
//using Ticketing.Models.Entities.UserEntity;
//using Ticketing.Shared.Enums;
//using Ticketing.Shared.Helpers;

//namespace Ticketing.Infrastructure.BaseInterfaceAndClass
//{
//    public sealed class RepositorySeedData
//    {
//        private readonly TicketingDbContext _dbContext;
//        private bool insertDataToDB = false;
//        private const string ADMIN_TITLE = "admin";

//        private const string NORMAL_USER_ROLE_TITLE = "Normal User";

//        private const string ADMIN_ROLE_TITLE = "Admin";
//        private const string ADMIN_ROLE_TITLE_PERSION = "مدیر";

//        private const string DEPARTMENT_USER_ROLE_TITLE = "Department User";
//        private const string DEPARTMENT_USER_ROLE_TITLE_PERSION = "کاربر واحد";

//        private const string DEPARTMENT_MANAGER_USER_ROLE_TITLE = "Department Manager User";
//        private const string DEPARTMENT_MANAGER_USER_ROLE_TITLE_PERSION = "مدیر واحد";

//        private const string CEO_USER_ROLE_TITLE = "CEO";
//        private const string CEO_USER_ROLE_TITLE_PERSION = "مدیر عامل سیستم";

//        public RepositorySeedData(TicketingDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public void SeedMethod(List<Tuple<string, string, string>> methodSummaries)
//        {
//            List<Role> checkRoles = _dbContext.Role.ToList();
//            if (!checkRoles.Any())
//            {
//                List<Role> newRoles = new List<Role>()
//                {
//                    new Role
//                    {
//                        Id = (byte)RoleAccesses.Admin,
//                        IsActive = true,
//                        EnglishTitle = ADMIN_ROLE_TITLE,
//                        PersianTitle = ADMIN_ROLE_TITLE_PERSION
//                    },
//                    new Role
//                    {
//                        Id = (byte)RoleAccesses.DepartmentUser,
//                        IsActive = true,
//                        EnglishTitle = DEPARTMENT_USER_ROLE_TITLE,
//                        PersianTitle = DEPARTMENT_USER_ROLE_TITLE_PERSION
//                    },
//                    new Role
//                    {
//                        Id = (byte)RoleAccesses.DepartmentManagerUser,
//                        IsActive = true,
//                        EnglishTitle = DEPARTMENT_MANAGER_USER_ROLE_TITLE,
//                        PersianTitle = DEPARTMENT_MANAGER_USER_ROLE_TITLE_PERSION
//                    },
//                    new Role
//                    {
//                        Id = (byte)RoleAccesses.CEOUser,
//                        IsActive = true,
//                        EnglishTitle = CEO_USER_ROLE_TITLE,
//                        PersianTitle = CEO_USER_ROLE_TITLE_PERSION
//                    }
//                };
//                insertDataToDB = true;
//                _dbContext.AddRange(newRoles);
//            }
//            List<UserInRole> userInRoles = _dbContext.UserInRole.Include(x => x.Role).Where(y => y.Role.EnglishTitle == ADMIN_TITLE || y.Role.EnglishTitle == NORMAL_USER_ROLE_TITLE).ToList();
//            User findUser = _dbContext.User.FirstOrDefault(x => x.UserName == ADMIN_TITLE);

//            if (!userInRoles.Any())
//            {
//                insertDataToDB = !userInRoles.Any();
//                UserInRole newUserInRole = new UserInRole();
//                if (findUser == null)
//                {
//                    User newUser = new User()
//                    {
//                        Id = Guid.NewGuid(),
//                        CreateDateTime = DateTime.Now,
//                        UpdateDateTime = DateTime.Now,
//                        MobileNumber = "09129876543",
//                        Password = "Aa@123456".EncryptText(),
//                        EmailAddress = "ticketingadmin@pns.co",
//                        IsActive = true,
//                        PureFullName = "مدیرسیستمتیکتینگ",
//                        FirstName = "مدیر سیستم",
//                        LastName = "تیکتینگ",
//                        UserName = ADMIN_TITLE,
//                    };

//                    newUser.CreateUserId = newUser.Id;
//                    newUser.LastUpdateUserId = newUser.Id;
//                    newUserInRole.UserId = newUser.Id;
//                    newUserInRole.RoleId = checkRoles.Any() ? checkRoles.FirstOrDefault(x => x.EnglishTitle == ADMIN_TITLE || x.EnglishTitle == NORMAL_USER_ROLE_TITLE).Id : (byte)RoleAccesses.Admin;
//                    _dbContext.AddRange(newUser, newUserInRole);
//                }
//                else if (findUser != null && !userInRoles.Any(x => x.UserId == findUser.Id))
//                {
//                    newUserInRole.UserId = findUser.Id;
//                    newUserInRole.RoleId = checkRoles.Any() ? checkRoles.FirstOrDefault(x => x.EnglishTitle == ADMIN_TITLE || x.EnglishTitle == NORMAL_USER_ROLE_TITLE).Id : (byte)RoleAccesses.Admin;
//                    _dbContext.Add(newUserInRole);
//                    insertDataToDB = true;
//                }
//            }

//            List<TicketStatus> ticketStatuses = _dbContext.TicketStatus.ToList();
//            if (!ticketStatuses.Any())
//            {
//                insertDataToDB = true;
//                List<TicketStatus> newTicketStatuses = new List<TicketStatus>() {
//                    new TicketStatus(){ Id = 1, EnglishName = "SetTicket", PersianName = "ایجاد شده", Color = "#FFFFFF" },
//                    new TicketStatus(){ Id = 2, EnglishName = "UnderProgress", PersianName = "در دست بررسی", Color = "#FFFFFF" },
//                    new TicketStatus(){ Id = 3, EnglishName = "Closed", PersianName = "بسته شده", Color = "#FFFFFF" }
//                };
//                _dbContext.AddRange(newTicketStatuses);
//            }

//            List<MenuItem> menuItems = _dbContext.MenuItem.ToList();
//            if (!menuItems.Any())
//            {
//                insertDataToDB = true;
//                List<MenuItem> newMenuItems = new List<MenuItem> {
//                    new MenuItem() { Title = "داشبورد", Order = 1, IsActive = true, RouteAddress = "ticketing/dashboard", MenuItemRoles = new List<MenuItemRole> () { new MenuItemRole() { RoleId = (int)RoleAccesses.Admin } } },
//                    new MenuItem() { Title = "منوها", Order = 2, IsActive = true, RouteAddress = "ticketing/menu-item/list", MenuItemRoles = new List<MenuItemRole> () { new MenuItemRole() { RoleId = (int)RoleAccesses.Admin } } }
//                };
//                _dbContext.AddRange(newMenuItems);
//            }

//            List<Resource> resources = _dbContext.Resources.Where(x => x.IsManualResource == false).ToList();
//            if (!resources.Any())
//            {
//                insertDataToDB = true;
//                foreach (var summary in methodSummaries)
//                {
//                    resources.Add(new Resource() { ActionName = summary.Item1, RouteAddress = summary.Item2, Description = summary.Item3, IsManualResource = false });
//                }
//                _dbContext.Resources.AddRange(resources);
//                _dbContext.SaveChanges();
//            }
//            else
//            {
//                var oldResource = resources.ExceptBy(methodSummaries.Select(x => x.Item1), x => x.ActionName).ToList();
//                var newResource = methodSummaries.ExceptBy(resources.Select(x => x.ActionName), x => x.Item1).ToList();

//                if (oldResource.Any())
//                {
//                    insertDataToDB = true;
//                    _dbContext.Resources.RemoveRange(oldResource);
//                }

//                oldResource = new List<Resource>();

//                if (newResource.Any())
//                {
//                    insertDataToDB = true;
//                    foreach (var summary in newResource)
//                    {
//                        oldResource.Add(new Resource() { ActionName = summary.Item1, RouteAddress = summary.Item2, Description = summary.Item3 });
//                    }
//                    _dbContext.Resources.AddRange(oldResource);
//                }
//            }

//            List<ResourceInRole> resourceInRoles = _dbContext.ResourceInRoles.ToList();
//            if (!resourceInRoles.Any())
//            {
//                insertDataToDB = true;
//                foreach (var resource in resources)
//                {
//                    resourceInRoles.Add(new ResourceInRole() { ResourceId = resource.Id, RoleId = (int)RoleAccesses.Admin });
//                }

//                _dbContext.ResourceInRoles.AddRange(resourceInRoles);
//            }

//            if (insertDataToDB)
//                _dbContext.SaveChanges();
//        }
//    }
//}
