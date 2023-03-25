using System;
using Acc.Server.Results.Companion.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acc.Server.Results.Companion.Database.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasData(new Car
                        {
                            Id = 1,
                            Class = "GT3",
                            AccModelName = "porsche_991_gt3_r",
                            MaxFuel = 120,
                            MaxRpm = 9250,
                            AccModelId = 0,
                            Name = "Porsche 991 GT3 R",
                            Year = 2018
                        },
            new Car
            {
                Id = 2,
                Class = "GT3",
                AccModelName = "mercedes_amg_gt3",
                MaxFuel = 120,
                MaxRpm = 7900,
                AccModelId = 1,
                Name = "Mercedes-AMG GT3",
                Year = 2015
            },
            new Car
            {
                Id = 3,
                Class = "GT3",
                AccModelName = "ferrari_488_gt3",
                MaxFuel = 110,
                MaxRpm = 7300,
                AccModelId = 2,
                Name = "Ferrari 488 GT3",
                Year = 2018
            },
            new Car
            {
                Id = 4,
                Class = "GT3",
                AccModelName = "audi_r8_lms",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 3,
                Name = "Audi R8 LMS",
                Year = 2015
            },
            new Car
            {
                Id = 5,
                Class = "GT3",
                AccModelName = "lamborghini_huracán_gt3",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 4,
                Name = "Lamborghini Huracán GT3",
                Year = 2015
            },
            new Car
            {
                Id = 6,
                Class = "GT3",
                AccModelName = "mclaren_650s_gt3",
                MaxFuel = 125,
                MaxRpm = 7500,
                AccModelId = 5,
                Name = "McLaren 650S GT3",
                Year = 2015
            },
            new Car
            {
                Id = 7,
                Class = "GT3",
                AccModelName = "nissan_gt_r_nismo_gt3",
                MaxFuel = 132,
                MaxRpm = 7500,
                AccModelId = 6,
                Name = "Nissan GT-R Nismo GT3",
                Year = 2018
            },
            new Car
            {
                Id = 8,
                Class = "GT3",
                AccModelName = "bmw_m6_gt3",
                MaxFuel = 125,
                MaxRpm = 7100,
                AccModelId = 7,
                Name = "BMW M6 GT3",
                Year = 2017
            },
            new Car
            {
                Id = 9,
                Class = "GT3",
                AccModelName = "bentley_continental_gt3_2018",
                MaxFuel = 132,
                MaxRpm = 7400,
                AccModelId = 8,
                Name = "Bentley Continental GT3",
                Year = 2018
            },
            new Car
            {
                Id = 10,
                Class = "GTC",
                AccModelName = "porsche_991ii_gt3_cup",
                MaxFuel = 100,
                MaxRpm = 8500,
                AccModelId = 9,
                Name = "Porsche 991 II GT3 Cup",
                Year = 2017
            },
            new Car
            {
                Id = 11,
                Class = "GT3",
                AccModelName = "nissan_gt_r_nismo_gt3",
                MaxFuel = 120,
                MaxRpm = 7500,
                AccModelId = 10,
                Name = "Nissan GT-R Nismo GT3",
                Year = 2015
            },
            new Car
            {
                Id = 12,
                Class = "GT3",
                AccModelName = "bentley_continental_gt3_2016",
                MaxFuel = 132,
                MaxRpm = 7500,
                AccModelId = 11,
                Name = "Bentley Continental GT3",
                Year = 2016
            },
            new Car
            {
                Id = 13,
                Class = "GT3",
                AccModelName = "amr_v12_vantage_gt3",
                MaxFuel = 132,
                MaxRpm = 7750,
                AccModelId = 12,
                Name = "AMR V12 Vantage GT3",
                Year = 2013
            },
            new Car
            {
                Id = 14,
                Class = "GT3",
                AccModelName = "lamborghini_gallardo_rex",
                MaxFuel = 130,
                MaxRpm = 8650,
                AccModelId = 13,
                Name = "Reiter Engineering R-EX GT3",
                Year = 2017
            },
            new Car
            {
                Id = 15,
                Class = "GT3",
                AccModelName = "jaguar_g3",
                MaxFuel = 119,
                MaxRpm = 8750,
                AccModelId = 14,
                Name = "Emil Frey Jaguar G3",
                Year = 2012
            },
            new Car
            {
                Id = 16,
                Class = "GT3",
                AccModelName = "lexus_rc_f_gt3",
                MaxFuel = 120,
                MaxRpm = 7750,
                AccModelId = 15,
                Name = "Lexus RC F GT3",
                Year = 2016
            },
            new Car
            {
                Id = 17,
                Class = "GT3",
                AccModelName = "lamborghini_huracan_gt3_evo",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 16,
                Name = "Lamborghini Huracan GT3 Evo",
                Year = 2019
            },
            new Car
            {
                Id = 18,
                Class = "GT3",
                AccModelName = "honda_nsx_gt3",
                MaxFuel = 120,
                MaxRpm = 7500,
                AccModelId = 17,
                Name = "Honda NSX GT3",
                Year = 2017
            },
            new Car
            {
                Id = 19,
                Class = "GTC",
                AccModelName = "lamborghini_huracan_st",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 18,
                Name = "Lamborghini Huracan SuperTrofeo",
                Year = 2015
            },
            new Car
            {
                Id = 20,
                Class = "GT3",
                AccModelName = "audi_r8_lms_evo",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 19,
                Name = "Audi R8 LMS Evo",
                Year = 2019
            },
            new Car
            {
                Id = 21,
                Class = "GT3",
                AccModelName = "amr_v8_vantage_gt3",
                MaxFuel = 120,
                MaxRpm = 7250,
                AccModelId = 20,
                Name = "AMR V8 Vantage",
                Year = 2019
            },
            new Car
            {
                Id = 22,
                Class = "GT3",
                AccModelName = "honda_nsx_gt3_evo",
                MaxFuel = 120,
                MaxRpm = 7650,
                AccModelId = 21,
                Name = "Honda NSX GT3 Evo",
                Year = 2019
            },
            new Car
            {
                Id = 23,
                Class = "GT3",
                AccModelName = "mclaren_720s_gt3",
                MaxFuel = 125,
                MaxRpm = 7700,
                AccModelId = 22,
                Name = "McLaren 720S GT3",
                Year = 2019
            },
            new Car
            {
                Id = 24,
                Class = "GT3",
                AccModelName = "porsche_991ii_gt3_r",
                MaxFuel = 120,
                MaxRpm = 9250,
                AccModelId = 23,
                Name = "Porsche 991 II GT3 R",
                Year = 2019
            },
            new Car
            {
                Id = 25,
                Class = "GT3",
                AccModelName = "ferrari_488_gt3_evo",
                MaxFuel = 120,
                MaxRpm = 7600,
                AccModelId = 24,
                Name = "Ferrari 488 GT3 Evo",
                Year = 2020
            },
            new Car
            {
                Id = 26,
                Class = "GT3",
                AccModelName = "mercedes_amg_gt3_evo",
                MaxFuel = 120,
                MaxRpm = 7600,
                AccModelId = 25,
                Name = "Mercedes-AMG GT3",
                Year = 2020
            },
            new Car
            {
                Id = 27,
                Class = "GTC",
                AccModelName = "ferrari_488_challenge_evo",
                MaxFuel = 120,
                MaxRpm = 8000,
                AccModelId = 26,
                Name = "Ferrari 488 Challenge Evo",
                Year = 2020
            },
            new Car
            {
                Id = 28,
                Class = "TCX",
                AccModelName = "bmw_m2_cs_racing",
                MaxFuel = 120,
                MaxRpm = 7520,
                AccModelId = 27,
                Name = "BMW M2 Club Sport Racing",
                Year = 2020
            },
            new Car
            {
                Id = 29,
                Class = "GTC",
                AccModelName = "porsche_992_gt3_cup",
                MaxFuel = 120,
                MaxRpm = 8750,
                AccModelId = 28,
                Name = "Porsche 992 GT3 Cup",
                Year = 2021
            },
            new Car
            {
                Id = 30,
                Class = "GTC",
                AccModelName = "lamborghini_huracan_st_evo2",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 29,
                Name = "Lamborghini Huracan SuperTrofeo EVO2",
                Year = 2021
            },
            new Car
            {
                Id = 31,
                Class = "GT3",
                AccModelName = "bmw_m4_gt3",
                MaxFuel = 120,
                MaxRpm = 7000,
                AccModelId = 30,
                Name = "BMW M4 GT3",
                Year = 2022
            },
            new Car
            {
                Id = 32,
                Class = "GT3",
                AccModelName = "audi_r8_lms_evo_ii",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 31,
                Name = "Audi R8 LMS GT3 Evo 2",
                Year = 2022
            },
            new Car
            {
                Id = 33,
                Class = "GT4",
                AccModelName = "alpine_a110_gt4",
                MaxFuel = 120,
                MaxRpm = 6450,
                AccModelId = 50,
                Name = "Alpine A110 GT4",
                Year = 2018
            },
            new Car
            {
                Id = 34,
                Class = "GT4",
                AccModelName = "amr_v8_vantage_gt4",
                MaxFuel = 120,
                MaxRpm = 7000,
                AccModelId = 51,
                Name = "Aston Martin Vantage GT4",
                Year = 2018
            },
            new Car
            {
                Id = 35,
                Class = "GT4",
                AccModelName = "audi_r8_gt4",
                MaxFuel = 120,
                MaxRpm = 8650,
                AccModelId = 52,
                Name = "Audi R8 GT4",
                Year = 2018
            },
            new Car
            {
                Id = 36,
                Class = "GT4",
                AccModelName = "bmw_m4_gt4",
                MaxFuel = 120,
                MaxRpm = 7600,
                AccModelId = 53,
                Name = "BMW M4 GT4",
                Year = 2018
            },
            new Car
            {
                Id = 37,
                Class = "GT4",
                AccModelName = "chevrolet_camaro_gt4",
                MaxFuel = 120,
                MaxRpm = 7500,
                AccModelId = 55,
                Name = "Chevrolet Camaro GT4",
                Year = 2017
            },
            new Car
            {
                Id = 38,
                Class = "GT4",
                AccModelName = "ginetta_g55_gt4",
                MaxFuel = 120,
                MaxRpm = 7200,
                AccModelId = 56,
                Name = "Ginetta G55 GT4",
                Year = 2012
            },
            new Car
            {
                Id = 39,
                Class = "GT4",
                AccModelName = "ktm_xbow_gt4",
                MaxFuel = 120,
                MaxRpm = 6500,
                AccModelId = 57,
                Name = "KTM X-Bow GT4",
                Year = 2016
            },
            new Car
            {
                Id = 40,
                Class = "GT4",
                AccModelName = "maserati_mc_gt4",
                MaxFuel = 120,
                MaxRpm = 7000,
                AccModelId = 58,
                Name = "Maserati MC GT4",
                Year = 2016
            },
            new Car
            {
                Id = 41,
                Class = "GT4",
                AccModelName = "mclaren_570s_gt4",
                MaxFuel = 120,
                MaxRpm = 7600,
                AccModelId = 59,
                Name = "McLaren 570S GT4",
                Year = 2016
            },
            new Car
            {
                Id = 42,
                Class = "GT4",
                AccModelName = "mercedes_amg_gt4",
                MaxFuel = 120,
                MaxRpm = 7000,
                AccModelId = 60,
                Name = "Mercedes AMG GT4",
                Year = 2016
            },
            new Car
            {
                Id = 43,
                Class = "GT4",
                AccModelName = "porsche_718_cayman_gt4_mr",
                MaxFuel = 120,
                MaxRpm = 7800,
                AccModelId = 61,
                Name = "Porsche 718 Cayman GT4 Clubsport",
                Year = 2019
            });
    }
}