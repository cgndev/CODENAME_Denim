namespace Denim.DataAccess.Migrations
{
    using Denim.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Denim.DataAccess.DenimDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Denim.DataAccess.DenimDbContext context)
        {

            context.Departments.AddOrUpdate(
                d => d.Name,
                    new Department { Name = "����" },
                    new Department { Name = "����" },
                    new Department { Name = "VFX" },
                    new Department { Name = "Set" },
                    new Department { Name = "Motion" },
                    new Department { Name = "Typo" },
                    new Department { Name = "Logo" },
                    new Department { Name = "Brand" }
                    );

            context.SaveChanges();

            context.Members.AddOrUpdate(
                m => m.FirstName,
                    new Member
                    {
                        LastName = "��",
                        FirstName = "����",
                        OfficeId = "872683",
                        DepartmentId = context.Departments.Where(d => d.Name == "����").FirstOrDefault().Id,
                        IpNumber = 129,
                        IsAllowMotion = true,
                        IsAllowBrand = true,
                        IsAllowLogo = true,
                        IsAllowTypo = true,
                        IsAllowVFX = true
                    },
                    new Member
                    {
                        LastName = "ȫ",
                        FirstName = "����",
                        OfficeId = "E07331",
                        DepartmentId = context.Departments.Where(d => d.Name == "����").FirstOrDefault().Id,
                        IpNumber = 199,
                        IsAllowMotion = true,
                        IsAllowBrand = true,
                        IsAllowLogo = true,
                        IsAllowTypo = true,
                        IsAllowVFX = true
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "�ټ�",
                        OfficeId = "E07584",
                        DepartmentId = context.Departments.Where(d => d.Name == "����").FirstOrDefault().Id,
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "����",
                        OfficeId = "E07790",
                        DepartmentId = context.Departments.Where(d => d.Name == "����").FirstOrDefault().Id,
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "���",
                        OfficeId = "010993",
                        DepartmentId = context.Departments.Where(d => d.Name == "VFX").FirstOrDefault().Id,
                        IpNumber = 127,
                        IsAllowMotion = true,
                        IsAllowBrand = false,
                        IsAllowLogo = false,
                        IsAllowTypo = false,
                        IsAllowVFX = true
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "����",
                        OfficeId = "050021",
                        DepartmentId = context.Departments.Where(d => d.Name == "Set").FirstOrDefault().Id,
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "���",
                        OfficeId = "000071",
                        DepartmentId = context.Departments.Where(d => d.Name == "Motion").FirstOrDefault().Id,
                        IpNumber = 151,
                        IsAllowMotion = true,
                        IsAllowBrand = false,
                        IsAllowLogo = false,
                        IsAllowTypo = false,
                        IsAllowVFX = false
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "����",
                        OfficeId = "020352",
                        DepartmentId = context.Departments.Where(d => d.Name == "Typo").FirstOrDefault().Id,
                        IpNumber = 170,
                        IsAllowMotion = false,
                        IsAllowBrand = false,
                        IsAllowLogo = true,
                        IsAllowTypo = false,
                        IsAllowVFX = false
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "����",
                        OfficeId = "020349",
                        DepartmentId = context.Departments.Where(d => d.Name == "Logo").FirstOrDefault().Id,
                        IpNumber = 189,
                        IsAllowMotion = false,
                        IsAllowBrand = false,
                        IsAllowLogo = true,
                        IsAllowTypo = false,
                        IsAllowVFX = false
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "�°�",
                        OfficeId = "000244",
                        DepartmentId = context.Departments.Where(d => d.Name == "Brand").FirstOrDefault().Id,
                        IpNumber = 200,
                        IsAllowMotion = false,
                        IsAllowBrand = true,
                        IsAllowLogo = false,
                        IsAllowTypo = false,
                        IsAllowVFX = false
                    },
                    new Member
                    {
                        LastName = "��",
                        FirstName = "��ö",
                        OfficeId = "130488",
                        DepartmentId = context.Departments.Where(d => d.Name == "VFX").FirstOrDefault().Id,
                        IpNumber = 111,
                        IsAllowMotion = true,
                        IsAllowBrand = true,
                        IsAllowLogo = false,
                        IsAllowTypo = false,
                        IsAllowVFX = true
                    }
                );


            context.SaveChanges();

            context.Friends.AddOrUpdate(
                f => f.FirstName,
                            new Friend { FirstName = "��ö", LastName = "��" },
                            new Friend { FirstName = "���", LastName = "��" },
                            new Friend { FirstName = "����", LastName = "��" },
                            new Friend { FirstName = "�ּ�", LastName = "��" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "F#" },
                new ProgrammingLanguage { Name = "Python" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Java" });

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new FriendPhoneNumber { Number = "+49 12345678", FriendId = context.Friends.First().Id });

            context.Meetings.AddOrUpdate(m => m.Title,
                new Meeting
                {
                    Title = "Watching Soccer",
                    DateFrom = new DateTime(2018, 5, 26),
                    DateTo = new DateTime(2018, 5, 26),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "����" && f.LastName == "��"),
                        context.Friends.Single(f => f.FirstName == "��ö" && f.LastName == "��")
                    }
                });

        }

    }
}
