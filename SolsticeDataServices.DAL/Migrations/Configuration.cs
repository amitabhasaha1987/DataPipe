namespace SolsticeDataServices.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SolsticeDataServices.DAL.Context.SolsticeDataServicesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "SolsticeDataServices.DAL.Context.SolsticeDataServicesContext";
        }

        protected override void Seed(SolsticeDataServices.DAL.Context.SolsticeDataServicesContext context)
        {            

            //context.MstrDevice.AddOrUpdate(
            //    d => new { d.DeviceId, d.DeviceName, d.IsLockActive },
            //         new Entities.MasterDevice { DeviceId = "d635dcd1-fa77-4e8f-850f-cd6fe2473ef5", DeviceName = "Terso QA", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "f01967f7-433e-4195-9f4c-ab225c24bbce", DeviceName = "TS001, Freezer, US, 2.4GHz, MF, 5.0 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "d80903fb-dc10-4d6e-9958-310ed1a21ecd", DeviceName = "TS002, Cabinet, US, 2.4GHz, MF, 12.0 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "118043fa-a5d2-4b0d-95a1-976f72a486fc", DeviceName = "TS003, Freezer, EU, 2.4GHz, MF, 4.3 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "22d8cee1-c468-4dd8-a40a-74c6f4df5d56", DeviceName = "TS012, Cabinet, EU, 2.4GHz MF, 5.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "bc832e54-e1c0-4be2-ad58-0337a0d267ef", DeviceName = "TS013, Freezer, US, 2.4GHz, MF, 5.4 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "47381b45-8f38-450c-8659-d81041f3c04d", DeviceName = "TS014, Freezer, AU, 2.4GHz, MF, 4.3 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "5524d003-129d-46fa-b175-c888eb682a5a", DeviceName = "TS015, Cabinet, EU, 2.4GHz, MF, 5.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "3f88a850-a415-4f43-9983-d82265217e27", DeviceName = "TS017, Freezer, US, UHF, MF, 5.4 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "33e2de8e-53d3-4006-a40a-af15f1da9d6d", DeviceName = "TS024, Cabinet, US, UHF, MF, 5.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "7bf0de03-f514-43ba-83f4-df95c0ea4cf5", DeviceName = "TS029, Refrigerator, US, UHF, MF, 6.1 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "25acbfbe-df76-4139-a117-4a57477413a2", DeviceName = "TS030, Cabinet, US, UHF, GF, 32.5 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "4032f064-94e1-4f4b-9dfb-f30c9ab830ae", DeviceName = "TS031, Freezer, US, UHF, MF, 9.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "ce14a2ad-09e2-4bcb-967c-d0ee191e704c", DeviceName = "TS032, Cabinet, US, UHF, GF, 7.9 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "2bb0c530-4c70-497a-876a-fc2521fece85", DeviceName = "TS034, Freezer, EU, UHF, MF, 4.3 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "6aca0fcb-13cf-4dbe-ab13-8e45049503e3", DeviceName = "TS035, Cabinet, EU, UHF, MF, 7.9 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "7749ca59-aa25-4e4d-a2ee-98c7ed8e0848", DeviceName = "TS036, Freezer, US, UHF, MF, 5.5cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "77b8dc6f-5634-4a0b-bf82-62bf0c8812e9", DeviceName = "TS037, Cabinet, AU, UHF, GF, 6.2 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "ddd01b14-dee8-488f-b1af-e126c5043c76", DeviceName = "TS038, Freezer, AU, UHF, MF, 5.5 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "b36faa8c-6d80-4be9-b004-aed268ddd94c", DeviceName = "TS039, Freezer, CN, UHF, MF, 4.3 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "a12121e7-1914-498e-9c0d-7f793912ef2b", DeviceName = "TS042, Freezer, AU, UHF, MF, 9.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "d2958bf2-e8e1-419a-a15b-3e1d904c1f89", DeviceName = "TS043, Freezer, EU, UHF, MF, 9.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "59c58289-dae1-44c0-88b9-fc192324e21a", DeviceName = "TS047, Freezer, EU, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "23c0f504-7a68-4698-a134-25807086c7c2", DeviceName = "TS048, Freezer, SG, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "73b28aab-76bf-4d91-a795-df48dd28f326", DeviceName = "TS049, Freezer, KR, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "0f8ce9f7-5a78-4117-89f9-fb7a90311b5f", DeviceName = "TS050, Cabinet, SG, UHF, GF, 7.9 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "e5abfa64-7d11-4894-b47c-579412cbf808", DeviceName = "TS051, Cabinet, KR, UHF, GF, 7.9 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "32df8b56-58aa-4ac8-bb80-b9c443d5754c", DeviceName = "TS052 Freezer, SG, UHF, MF, 9.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "798ee81e-140f-400a-a8b7-775c12c7d05c", DeviceName = "TS053 Freezer, KR, UHF, MF, 9.6 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "14c30463-4e98-40c4-9b79-854f6706f064", DeviceName = "TS054, Freezer, AU, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "8f662f4a-05e2-4e43-a42a-4c522d4e7df7", DeviceName = "TS055, Refrigerator, EU, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "810dcd5d-396c-4ec8-a859-1e465696510f", DeviceName = "TS056, Cabinet, EU, UHF, GF, 25 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "7e756c88-6144-4fb9-a1a0-3a1a72b74853", DeviceName = "TS057, ULT Freezer, US, UHF, MF, 14.9 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "97d9fc36-8392-43ea-a3e3-48136b66f612", DeviceName = "TS058, Freezer, BR, UHF, MF, 9.7cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "f8facfd2-2db3-4e8e-b420-90142cd29a03", DeviceName = "TS059, Cabinet, BR, UHF, GF, 25 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "b6df10e4-3719-453c-80f9-539e51578091", DeviceName = "TS060, Refrigerator, EU, UHF, GF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "49f374f1-6e74-4452-ac35-c140f85f4520", DeviceName = "TS061, Refrigerator, AU, UHF, GF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "ef61cdc2-7e6c-4ed1-a72e-425c13fb56fb", DeviceName = "TS066, Kiosk", IsLockActive = false },
            //         new Entities.MasterDevice { DeviceId = "b392cec2-f61e-4b74-81c2-d050a74f571f", DeviceName = "TS068, Freezer, PA, UHF, MF, 3.7 cu ft", IsLockActive = true },
            //         new Entities.MasterDevice { DeviceId = "fd21b70d-3148-4b37-92a5-ed3f0263fbe2", DeviceName = "TS074, Refrigerator, US, UHF, GF, 20 cu ft", IsLockActive = true }
            //);

            //context.Users.AddOrUpdate(
            //    u => new { u.FirstName, u.LastName, u.Company, u.Contact, u.EmailId, u.Password, u.LinkedAccessKey, u.DeviceAccessKey, u.IsBlocked, u.IsSuperAdmin },
            //         new Entities.Users
            //         {
            //             FirstName = "abc",
            //             LastName = "def",
            //             Company = "abc company",
            //             Contact = "9876543210",
            //             EmailId = "admin@admin.com",
            //             Password = "admin",
            //             LinkedAccessKey = "1CE3F681-6351-4680-ABD6-55F60E05C2ED",
            //             DeviceAccessKey = "4B178187-0BCA-411E-8461-981A33C36D0B",
            //             IsBlocked = false,
            //             IsSuperAdmin = false
            //         },
            //         new Entities.Users
            //         {
            //             FirstName = "Super",
            //             LastName = "Admin",
            //             Company = "Super Comany",
            //             Contact = "1234567890",
            //             EmailId = "sadmin@sadmin.com",
            //             Password = "sadmin",
            //             LinkedAccessKey = "AF8B7D0C-C06F-4E04-BB4F-7E64F43DD496",
            //             IsBlocked = false,
            //             IsSuperAdmin = true
            //         }
            //    );

            //context.Region.AddOrUpdate(
            //    r => new { r.RegionId, r.RegionName },
            //         new Entities.Region { RegionName = "US", RegionId = "US" },
            //         new Entities.Region { RegionName = "EU", RegionId = "EU" },
            //         new Entities.Region { RegionName = "AP", RegionId = "AP" }
            //    );
        }
    }
}
