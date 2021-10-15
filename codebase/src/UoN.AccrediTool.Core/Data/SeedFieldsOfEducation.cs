using Microsoft.EntityFrameworkCore;

using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Data
{
    public partial class DataContext
    {
        private static void SeedFieldsOfEducation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UoFieldOfEducationModel>().HasData
            (
                #region Natural and Physical Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 1,
                    FieldCode = "01",
                    Name = "Natural and Physical Sciences",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Natural and Physical Sciences is the study of all living organisms and inanimate natural objects, through experiment, observation and deduction."
                },
                #region Mathematical Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 2,
                    FieldCode = "0101",
                    Name = "Mathematical Sciences",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Mathematical Sciences is the study of abstract deductive systems, numerical facts, data and their applications.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 3,
                    FieldCode = "010101",
                    Name = "Mathematics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mathematics is the study of deductive systems, including algebra, arithmetic, geometry, analysis and applied mathematics.",
                    ParentFieldOfEducationId = 2
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 4,
                    FieldCode = "010103",
                    Name = "Statistics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Statistics is the study of collecting, describing, arranging and analysing numerical data.",
                    ParentFieldOfEducationId = 2
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 5,
                    FieldCode = "010199",
                    Name = "Mathematical Sciences, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Mathematical Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 2
                },
                #endregion
                #region Physics and Astronomy
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 6,
                    FieldCode = "0103",
                    Name = "Physics and Astronomy",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Physics and Astronomy is the study of the laws governing the structure of the universe and the forms of matter and energy.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 7,
                    FieldCode = "010301",
                    Name = "Physics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Physics is the study of the laws governing states and properties of matter and energy.",
                    ParentFieldOfEducationId = 6
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 8,
                    FieldCode = "010303",
                    Name = "Astronomy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Astronomy is the study of celestial bodies, their positions, motions, distances and physical conditions, and their origins and evolution.",
                    ParentFieldOfEducationId = 6
                },
                #endregion
                #region Chemical Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 9,
                    FieldCode = "0105",
                    Name = "Chemical Sciences",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Chemical Sciences is the study of the composition, structure, and the chemical transformations of matter.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 10,
                    FieldCode = "010501",
                    Name = "Organic Chemistry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Organic Chemistry is the study of the description, properties, reactions and preparations of carbon compounds.",
                    ParentFieldOfEducationId = 9
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 11,
                    FieldCode = "010503",
                    Name = "Inorganic Chemistry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Inorganic Chemistry is the study of the description, properties, reactions, and preparation of all the elements and their compounds, with the exception of carbon compounds.",
                    ParentFieldOfEducationId = 9
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 12,
                    FieldCode = "010599",
                    Name = "Chemical Sciences, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Chemical Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 9
                },
                #endregion
                #region Earth Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 13,
                    FieldCode = "0107",
                    Name = "Earth Sciences",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Earth Sciences is the study of the nature, composition and structure of the Earth including its atmosphere and hydrosphere.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 14,
                    FieldCode = "010701",
                    Name = "Atmospheric Sciences",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Atmospheric Sciences is the study of the composition and structure of the earth’s atmosphere and climate.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 15,
                    FieldCode = "010703",
                    Name = "Geology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Geology is the study of origin, composition and structure of the earth.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 16,
                    FieldCode = "010705",
                    Name = "Geophysics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Geophysics is the study of the physical properties of the Earth through collecting and interpreting gravitational, magnetic, seismic and electrical data.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 17,
                    FieldCode = "010707",
                    Name = "Geochemistry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Geochemistry is the study of the quantities and distribution of the chemical elements in minerals, ores, rocks, soils, water, and the atmosphere.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 18,
                    FieldCode = "010709",
                    Name = "Soil Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Soil Science is the study of the origins, composition, structure and classification of soils.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 19,
                    FieldCode = "010711",
                    Name = "Hydrology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Hydrology is the study of the location and movement of inland water, both frozen and liquid, above and below ground.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 20,
                    FieldCode = "010713",
                    Name = "Oceanography",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Oceanography is the study of the origins, composition, structure and history of the oceans and the ocean floor.",
                    ParentFieldOfEducationId = 13
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 21,
                    FieldCode = "010799",
                    Name = "Earth Sciences, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Earth Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 13
                },
                #endregion
                #region Biological Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 22,
                    FieldCode = "0109",
                    Name = "Biological Sciences",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Biological Sciences is the study of the structure, function, reproduction, growth, evolution and behaviour of living organisms.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 23,
                    FieldCode = "010901",
                    Name = "Biochemistry and Cell Biology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Biochemistry and Cell Biology is the study of the chemistry of living organisms and the structure and function of cells.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 24,
                    FieldCode = "010903",
                    Name = "Botany",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Botany is the study of plants.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 25,
                    FieldCode = "010905",
                    Name = "Ecology and Evolution",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Ecology and Evolution is the study of interactions between organisms and their environment, and the processes of biological change in organisms.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 26,
                    FieldCode = "010907",
                    Name = "Marine Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Marine Science is the study of marine and estuarine plants and wildlife. It includes freshwater science.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 27,
                    FieldCode = "010909",
                    Name = "Genetics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Genetics is the study of heredity and the units of biological inheritance.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 28,
                    FieldCode = "010911",
                    Name = "Microbiology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Microbiology is the study of microscopic forms of life such as bacteria, viruses and protozoa.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 29,
                    FieldCode = "010913",
                    Name = "Human Biology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Human Biology is the study of human and primate anatomy, physiology, evolution and biosocial interactions.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 30,
                    FieldCode = "010915",
                    Name = "Zoology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Zoology is the study of animals and insects.",
                    ParentFieldOfEducationId = 22
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 31,
                    FieldCode = "010999",
                    Name = "Biological Sciences, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Biological Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 22
                },
                #endregion
                #region Other Natural and Physical Sciences
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 32,
                    FieldCode = "0199",
                    Name = "Other Natural and Physical Sciences",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Natural and Physical Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 1
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 33,
                    FieldCode = "019901",
                    Name = "Medical Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Medical Science is the study of the application of physics, biology and chemistry to medicine.",
                    ParentFieldOfEducationId = 32
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 34,
                    FieldCode = "019903",
                    Name = "Forensic Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Forensic Science is the study of the application of scientific techniques to criminal investigations.",
                    ParentFieldOfEducationId = 32
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 35,
                    FieldCode = "019905",
                    Name = "Food Science and Biotechnology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Food Science and Biotechnology is the study of the physical and chemical properties of food, and the industrial use of living organisms to produce food, pharmaceuticals and other products.",
                    ParentFieldOfEducationId = 32
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 36,
                    FieldCode = "019907",
                    Name = "Pharmacology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Pharmacology is the study of the development, uses and effects of drugs.",
                    ParentFieldOfEducationId = 32
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 37,
                    FieldCode = "019909",
                    Name = "Laboratory Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Laboratory Technology is the study of the procedures, techniques and equipment used in biological, chemical, medical and other laboratories.",
                    ParentFieldOfEducationId = 32
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 38,
                    FieldCode = "019999",
                    Name = "Natural and Physical Sciences, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Natural and Physical Sciences not elsewhere classified.",
                    ParentFieldOfEducationId = 32
                },
                #endregion
                #endregion
                #region Information Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 39,
                    FieldCode = "02",
                    Name = "Information Technology",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Information Technology is the study of the processing, transmitting and storage of information by computers."
                },
                #region Computer Science
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 40,
                    FieldCode = "0201",
                    Name = "Computer Science",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Computer Science is the study of the design and development of computer systems.",
                    ParentFieldOfEducationId = 39
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 41,
                    FieldCode = "020101",
                    Name = "Formal Language Theory",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Formal Language Theory is the study of automated formal languages and the algorithms used to recognise them.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 42,
                    FieldCode = "020103",
                    Name = "Programming",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Programming is the study of writing coded instructions for computers to perform particular tasks.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 43,
                    FieldCode = "020105",
                    Name = "Computational Theory",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Computational Theory is the study of problems that can be solved using efficient algorithms and the identification of problems that are unsolvable.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 44,
                    FieldCode = "020107",
                    Name = "Compiler Construction",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Compiler Construction is the study of the theories and techniques for translating instructions between high and low level languages.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 45,
                    FieldCode = "020109",
                    Name = "Algorithms",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Algorithms is the study of the processes and rules that describe the logical sequence of operations to be performed by a program.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 46,
                    FieldCode = "020111",
                    Name = "Data Structures",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Data Structures is the study of the system of relationships between items of data which permit efficient manipulation through reducing complexity.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 47,
                    FieldCode = "020113",
                    Name = "Networks and Communications",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Networks and Communications is the study of linking computers for information exchange and distribution.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 48,
                    FieldCode = "020115",
                    Name = "Computer Graphics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Computer Graphics is the study of developing and programming graphical output devices to generate pictures.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 49,
                    FieldCode = "020117",
                    Name = "Operating Systems",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Operating Systems is the study of designing and constructing systems to control processes and process scheduling.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 50,
                    FieldCode = "020119",
                    Name = "Artificial Intelligence",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Artificial Intelligence is the study of creating computer programs which demonstrate some of the attributes associated with human intelligence.",
                    ParentFieldOfEducationId = 40
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 51,
                    FieldCode = "020199",
                    Name = "Computer Science, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Computer Science not elsewhere classified.",
                    ParentFieldOfEducationId = 40
                },
                #endregion
                #region Information Systems
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 52,
                    FieldCode = "0203",
                    Name = "Information Systems",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Information Systems is the study of the flow of information, capturing data, and the design and specification of information systems and user interfaces.",
                    ParentFieldOfEducationId = 39
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 53,
                    FieldCode = "020301",
                    Name = "Conceptual Modelling",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Conceptual Modelling is the study of representing the structure, type, and relationships of data elements within a system used to support system design.",
                    ParentFieldOfEducationId = 52
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 54,
                    FieldCode = "020303",
                    Name = "Database Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Database Management is the study of programs which create and maintain databases.",
                    ParentFieldOfEducationId = 52
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 55,
                    FieldCode = "020305",
                    Name = "Systems Analysis and Design",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Systems Analysis and Design is the study of analysing the information needs of the user and designing or modifying a system to meet these needs.",
                    ParentFieldOfEducationId = 52
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 56,
                    FieldCode = "020307",
                    Name = "Decision Support Systems",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Decision Support Systems is the study of designing information systems based on statistical and mathematical models to support management decisions.",
                    ParentFieldOfEducationId = 52
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 57,
                    FieldCode = "020399",
                    Name = "Information Systems, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Information Systems not elsewhere classified.",
                    ParentFieldOfEducationId = 52
                },
                #endregion
                #region Other Information Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 58,
                    FieldCode = "0299",
                    Name = "Other Information Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Information Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 39
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 59,
                    FieldCode = "029901",
                    Name = "Security Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Security Science is the study of securing electronic information and preventing unauthorised access to data and programs.",
                    ParentFieldOfEducationId = 58
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 60,
                    FieldCode = "029999",
                    Name = "Information Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Information Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 58
                },
                #endregion
                #endregion
                #region Engineering and Related Technologies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 61,
                    FieldCode = "03",
                    Name = "Engineering and Related Technologies",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Engineering and Related Technologies is the study of the design, manufacture, installation, maintenance and functioning of machines, systems and structures; and the composition and processing of metals, ceramics, foodstuffs and other materials. It includes the measurement and mapping of the earth’s surface and its natural and constructed features."
                },
                #region Manufacturing Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 62,
                    FieldCode = "0301",
                    Name = "Manufacturing Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Manufacturing Engineering and Technology is the study of the planning, organisation and operation of manufacturing methods, processes, facilities and systems.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 63,
                    FieldCode = "030101",
                    Name = "Manufacturing Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Manufacturing Engineering is the study of designing, developing and organising safe and flexible manufacturing processes, systems and facilities.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 64,
                    FieldCode = "030103",
                    Name = "Printing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Printing is the study of reproducing texts and pictorial works onto any media from original plates and masters, and producing finished publications. It includes electronic (desktop) publishing.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 65,
                    FieldCode = "030105",
                    Name = "Textile Making",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Textile Making is the study of the commercial production of textiles, yarns and fabrics.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 66,
                    FieldCode = "030107",
                    Name = "Garment Making",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Garment Making is the study of the commercial production of clothing and other apparel.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 67,
                    FieldCode = "030109",
                    Name = "Footwear Making",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Footwear Making is the study of designing, making and repairing shoes, boots and other footwear.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 68,
                    FieldCode = "030111",
                    Name = "Wood Machining and Turning",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Wood Machining and Turning is the study of shaping wood using various machines.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 69,
                    FieldCode = "030113",
                    Name = "Cabinet Making",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Cabinet Making is the study of making and repairing furniture and interior fittings for buildings.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 70,
                    FieldCode = "030115",
                    Name = "Furniture Upholstery and Renovation",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Furniture Upholstery and Renovation is the study of designing, making and repairing the soft furnishings of chairs, beds and other furniture.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 71,
                    FieldCode = "030117",
                    Name = "Furniture Polishing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Furniture Polishing is the study of preparing and polishing different timber furniture surfaces.",
                    ParentFieldOfEducationId = 62
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 72,
                    FieldCode = "030199",
                    Name = "Manufacturing Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Manufacturing Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 62
                },
                #endregion
                #region Process and Resources Engineering
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 73,
                    FieldCode = "0303",
                    Name = "Process and Resources Engineering",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Process and Resources Engineering is the study of planning, designing and developing systems, processes and plant for locating and extracting minerals, oil and gas from the earth, and for physically and chemically transforming raw materials to produce metals, alloys, petrochemicals, ceramics, polymers and other materials. It includes the industrial manufacture, processing, packaging and handling of foodstuffs, pharmaceuticals and biochemicals.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 74,
                    FieldCode = "030301",
                    Name = "Chemical Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Chemical Engineering is the study of planning, designing and developing products and processes where chemical and physical changes occur.",
                    ParentFieldOfEducationId = 73
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 75,
                    FieldCode = "030303",
                    Name = "Mining Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mining Engineering is the study of planning, developing, assessing, directing and managing the extraction of minerals, oil and gas from the earth.",
                    ParentFieldOfEducationId = 73
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 76,
                    FieldCode = "030305",
                    Name = "Materials Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Materials Engineering is the study of assaying, producing and refining materials, including metals, alloys, ceramics and polymers, timber, pulp and paper.",
                    ParentFieldOfEducationId = 73
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 77,
                    FieldCode = "030307",
                    Name = "Food Processing Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Food Processing Technology is the study of the industrial processing, packaging and handling of food.",
                    ParentFieldOfEducationId = 73
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 78,
                    FieldCode = "030399",
                    Name = "Process and Resources Engineering, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Process and Resources Engineering not elsewhere classified.",
                    ParentFieldOfEducationId = 73
                },
                #endregion
                #region Automotive Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 79,
                    FieldCode = "0305",
                    Name = "Automotive Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Automotive Engineering and Technology is the study of planning, designing, developing, producing and maintaining motor vehicles including earth moving equipment, motor cycles and small engines.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 80,
                    FieldCode = "030501",
                    Name = "Automotive Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Automotive Engineering is the study of designing, developing and testing motor vehicles, earth moving equipment, small engines and their components.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 81,
                    FieldCode = "030503",
                    Name = "Vehicle Mechanics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Vehicle Mechanics is the study of maintaining, diagnosing faults in, repairing and servicing motor vehicles and their components and small engines in boats, motorcycles, lawnmowers, generators and related equipment.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 82,
                    FieldCode = "030505",
                    Name = "Automotive Electrics and Electronics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Automotive Electrics and Electronics is the study of installing, maintaining and repairing electrical wiring and electronic components in motor vehicles, boats and earth moving equipment.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 83,
                    FieldCode = "030507",
                    Name = "Automotive Vehicle Refinishing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Automotive Vehicle Refinishing is the study of preparing vehicle surfaces, mixing and matching paint colours, spray painting and detailing motor vehicles and aircraft.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 84,
                    FieldCode = "030509",
                    Name = "Automotive Body Construction",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Automotive Body Construction is the study of building bodies for motor vehicles, trailers, buses, and railway rolling stock.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 85,
                    FieldCode = "030511",
                    Name = "Panel Beating",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Panel Beating is the study of repairing damaged motor vehicle bodies and replacing panels.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 86,
                    FieldCode = "030513",
                    Name = "Upholstery and Vehicle Trimming",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Upholstery and Vehicle Trimming is the study of making, installing, repairing and replacing the interior trimmings and upholstery of motor vehicles, boats, aircraft and railway carriages.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 87,
                    FieldCode = "030515",
                    Name = "Automotive Vehicle Operations",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Automotive Vehicle Operations is the study of driving motor vehicles including motor cycles, trucks and buses.",
                    ParentFieldOfEducationId = 79
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 88,
                    FieldCode = "030599",
                    Name = "Automotive Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Automotive Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 79
                },
                #endregion
                #region Mechanical and Industrial Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 89,
                    FieldCode = "0307",
                    Name = "Mechanical and Industrial Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Mechanical and Industrial Engineering and Technology is the study of designing, planning, installing, operating, maintaining and repairing mechanical plant, machinery and tools.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 90,
                    FieldCode = "030701",
                    Name = "Mechanical Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mechanical Engineering is the study of planning, designing and developing machines, mechanical plant and systems.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 91,
                    FieldCode = "030703",
                    Name = "Industrial Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Industrial Engineering is the study of planning, designing, organising and operating industrial facilities and processes for the economic, safe and effective use of physical and human resources.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 92,
                    FieldCode = "030705",
                    Name = "Toolmaking",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Toolmaking is the study of making and repairing tools, dies, jigs, fixtures and other precision parts and equipment.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 93,
                    FieldCode = "030707",
                    Name = "Metal Fitting, Turning and Machining",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Metal Fitting, Turning and Machining is the study of setting up machining tools, production machines and textile machines, operating machining tools and machines to shape metal stock and castings, and fitting and assembling the fabricated metal parts into products.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 94,
                    FieldCode = "030709",
                    Name = "Sheetmetal Working",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Sheetmetal Working is the study of cutting, shaping and joining sheetmetal and other materials, and using hand tools, power tools and machines to make products and components.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 95,
                    FieldCode = "030711",
                    Name = "Boilermaking and Welding",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Boilermaking and Welding is the study of marking out, cutting, shaping and joining metals, and constructing and repairing steelwork structures and pressure vessels including boilers, piping systems and ships.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 96,
                    FieldCode = "030713",
                    Name = "Metal Casting and Patternmaking",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Metal Casting and Patternmaking is the study of planning and fabricating mould patterns and cores for the production of metal castings.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 97,
                    FieldCode = "030715",
                    Name = "Precision Metalworking",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Precision Metalworking is the study of designing, fabricating, assembling, maintaining and repairing precision instruments such as locks, timepieces and firearms.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 98,
                    FieldCode = "030717",
                    Name = "Plant and Machine Operations",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Plant and Machine Operations is the study of setting up, controlling and monitoring mobile and stationary plant, equipment and machinery either directly or by remote control.",
                    ParentFieldOfEducationId = 89
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 99,
                    FieldCode = "030799",
                    Name = "Mechanical and Industrial Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Mechanical and Industrial Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 89
                },
                #endregion
                #region Civil Engineering
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 100,
                    FieldCode = "0309",
                    Name = "Civil Engineering",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Civil Engineering is the study of planning, designing, testing and directing the construction of large scale buildings and structures, and transport, water supply, pollution control and sewerage systems. It includes economic, functional and environmental considerations in the design and construction.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 101,
                    FieldCode = "030901",
                    Name = "Construction Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Construction Engineering is the study of designing and developing infrastructure such as buildings, roads, bridges, tunnels and quarries.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 102,
                    FieldCode = "030903",
                    Name = "Structural Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Structural Engineering is the study of the statical properties of structures and the behaviour and durability of materials used for erecting structures.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 103,
                    FieldCode = "030905",
                    Name = "Building Services Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Building Services Engineering is the study of designing and developing infrastructure aimed at enhancing human comfort through controlling heat, light and sound in built environments.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 104,
                    FieldCode = "030907",
                    Name = "Water and Sanitary Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Water and Sanitary Engineering is the study of designing and developing water storage and distribution systems, and sludge, effluent and waste water treatment systems.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 105,
                    FieldCode = "030909",
                    Name = "Transport Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Transport Engineering is the study of planning and developing efficient transport systems.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 106,
                    FieldCode = "030911",
                    Name = "Geotechnical Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Geotechnical Engineering is the study of analysing foundations, slopes and soil mechanics, and designing foundations.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 107,
                    FieldCode = "030913",
                    Name = "Ocean Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Ocean Engineering is the study of designing, constructing and maintaining coastal and ocean related projects and facilities, and designing floating, fixed and sub-sea offshore systems.",
                    ParentFieldOfEducationId = 100
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 108,
                    FieldCode = "030999",
                    Name = "Civil Engineering, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Civil Engineering not elsewhere classified.",
                    ParentFieldOfEducationId = 100
                },
                #endregion
                #region Geomatic Engineering
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 109,
                    FieldCode = "0311",
                    Name = "Geomatic Engineering",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Geomatic Engineering is the study of measuring and graphically representing natural and constructed features of the environment.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 110,
                    FieldCode = "031101",
                    Name = "Surveying",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Surveying is the study of measuring and representing the shape, contours, locations and dimensions of the constructed and natural features of the earth, in the form of reports and plans.",
                    ParentFieldOfEducationId = 109
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 111,
                    FieldCode = "031103",
                    Name = "Mapping Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mapping Science is the study of graphically representing the constructed and natural features of the earth in the form of maps.",
                    ParentFieldOfEducationId = 109
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 112,
                    FieldCode = "031199",
                    Name = "Geomatic Engineering, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Geomatic Engineering not elsewhere classified.",
                    ParentFieldOfEducationId = 109
                },
                #endregion
                #region Electrical and Electronic Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 113,
                    FieldCode = "0313",
                    Name = "Electrical and Electronic Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Electrical and Electronic Engineering and Technology is the study of planning, designing, developing, testing, installing and maintaining electrical, electronic and communications equipment, circuits and systems. It includes designing, installing and maintaining equipment for generating and distributing electrical power.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 114,
                    FieldCode = "031301",
                    Name = "Electrical Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Electrical Engineering is the study of planning, designing, developing and maintaining electrical equipment, circuits and systems.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 115,
                    FieldCode = "031303",
                    Name = "Electronic Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Electronic Engineering is the study of planning, designing, developing and maintaining electronic equipment, machinery and systems.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 116,
                    FieldCode = "031305",
                    Name = "Computer Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Computer Engineering is the study of designing and constructing digital data processing hardware.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 117,
                    FieldCode = "031307",
                    Name = "Communications Technologies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Communications Technologies is the study of communication transmission and signal systems.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 118,
                    FieldCode = "031309",
                    Name = "Communications Equipment Installation and Maintenance",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Communications Equipment Installation and Maintenance is the study of installing, maintaining, operating, diagnosing and repairing faults in telecommunications equipment, appliances, instruments and systems.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 119,
                    FieldCode = "031311",
                    Name = "Powerline Installation and Maintenance",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Powerline Installation and Maintenance is the study of installing, repairing, maintaining and monitoring overhead and underground electrical power distribution networks.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 120,
                    FieldCode = "031313",
                    Name = "Electrical Fitting, Electrical Mechanics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Electrical Fitting, Electrical Mechanics is the study of installing, maintaining, diagnosing faults in and repairing electrical wiring and equipment in domestic, commercial and industrial establishments, ships and trains.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 121,
                    FieldCode = "031315",
                    Name = "Refrigeration and Air Conditioning Mechanics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Refrigeration and Air Conditioning Mechanics is the study of installing, maintaining, diagnosing faults in and repairing domestic, commercial and industrial refrigeration, air conditioning and heating equipment.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 122,
                    FieldCode = "031317",
                    Name = "Electronic Equipment Servicing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Electronic Equipment Servicing is the study of maintaining, diagnosing faults in and repairing computers, radio and television receivers, audio, video, and other electronic business and domestic equipment.",
                    ParentFieldOfEducationId = 113
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 123,
                    FieldCode = "031399",
                    Name = "Electrical and Electronic Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Electrical and Electronic Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 113
                },
                #endregion
                #region Aerospace Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 124,
                    FieldCode = "0315",
                    Name = "Aerospace Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Aerospace Engineering and Technology is the study of planning, designing, developing, assembling and maintaining aircraft structures and systems. It includes operating and directing aircraft.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 125,
                    FieldCode = "031501",
                    Name = "Aerospace Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Aerospace Engineering is the study of designing, developing and modifying aircraft.",
                    ParentFieldOfEducationId = 124
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 126,
                    FieldCode = "031503",
                    Name = "Aircraft Maintenance Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Aircraft Maintenance Engineering is the study of assembling, maintaining and repairing aircraft structures, and avionic and mechanical systems.",
                    ParentFieldOfEducationId = 124
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 127,
                    FieldCode = "031505",
                    Name = "Aircraft Operation",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Aircraft Operation is the study of piloting and navigating aircraft.",
                    ParentFieldOfEducationId = 124
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 128,
                    FieldCode = "031507",
                    Name = "Air Traffic Control",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Air Traffic Control is the study of monitoring and directing ground and air aircraft movements.",
                    ParentFieldOfEducationId = 124
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 129,
                    FieldCode = "031599",
                    Name = "Aerospace Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Aerospace Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 124
                },
                #endregion
                #region Maritime Engineering and Technology
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 130,
                    FieldCode = "0317",
                    Name = "Maritime Engineering and Technology",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Maritime Engineering and Technology is the study of designing, maintaining and operating marine craft and shipboard machinery and systems.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 131,
                    FieldCode = "031701",
                    Name = "Maritime Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Maritime Engineering is the study of maintaining and operating shipboard machinery and systems.",
                    ParentFieldOfEducationId = 130
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 132,
                    FieldCode = "031703",
                    Name = "Marine Construction",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Marine Construction is the study of fabricating, fitting out and repairing marine vessels and their structural components.",
                    ParentFieldOfEducationId = 130
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 133,
                    FieldCode = "031705",
                    Name = "Marine Craft Operation",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Marine Craft Operation is the study of operating ships and other marine craft and their navigation and communication systems.",
                    ParentFieldOfEducationId = 130
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 134,
                    FieldCode = "031799",
                    Name = "Maritime Engineering and Technology, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Maritime Engineering and Technology not elsewhere classified.",
                    ParentFieldOfEducationId = 130
                },
                #endregion
                #region Other Engineering and Related Technologies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 135,
                    FieldCode = "0399",
                    Name = "Other Engineering and Related Technologies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Engineering and Related Technologies not elsewhere classified.",
                    ParentFieldOfEducationId = 61
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 136,
                    FieldCode = "039901",
                    Name = "Environmental Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Environmental Engineering is the study of technology concerned with the mitigation of pollution, contamination and deterioration of the environment.",
                    ParentFieldOfEducationId = 135
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 137,
                    FieldCode = "039903",
                    Name = "Biomedical Engineering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Biomedical Engineering is the study of designing and manufacturing medical devices and equipment to assist in overcoming physical disabilities.",
                    ParentFieldOfEducationId = 135
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 138,
                    FieldCode = "039905",
                    Name = "Fire Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Fire Technology is the study of fire detection, suppression and prevention methods and equipment.",
                    ParentFieldOfEducationId = 135
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 139,
                    FieldCode = "039907",
                    Name = "Rail Operations",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Rail Operations is the study of driving, shunting and marshalling trains.",
                    ParentFieldOfEducationId = 135
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 140,
                    FieldCode = "039909",
                    Name = "Cleaning",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Cleaning is the study of removing dirt and stains from, and maintaining and restoring clothing and fabrics, and domestic, industrial and commercial assets.",
                    ParentFieldOfEducationId = 135
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 141,
                    FieldCode = "039999",
                    Name = "Engineering and Related Technologies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Engineering and Related Technologies not elsewhere classified.",
                    ParentFieldOfEducationId = 135
                },
                #endregion
                #endregion
                #region Architecture and Building
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 142,
                    FieldCode = "04",
                    Name = "Architecture and Building",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Architecture and Building is the study of the art, science and techniques involved in designing, constructing, adapting and maintaining public, commercial, industrial and residential structures and landscapes. It includes the study of the art and science of designing and planning urban and regional environments."
                },
                #region Architecture and Urban Environment
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 143,
                    FieldCode = "0401",
                    Name = "Architecture and Urban Environment",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Architecture and Urban Environment is the study of the art and science of planning and designing public, commercial, industrial and residential buildings, and their interiors. It includes the study of planning and designing built environments.",
                    ParentFieldOfEducationId = 142
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 144,
                    FieldCode = "040101",
                    Name = "Architecture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Architecture is the study of the art, science and techniques of building design. It includes utilitarian ends, such as the soundness of the structure, the functional and economic efficiency of the building, and aesthetic considerations.",
                    ParentFieldOfEducationId = 143
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 145,
                    FieldCode = "040103",
                    Name = "Urban Design and Regional Planning",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Urban Design and Regional Planning is the study of designing and planning towns and rural communities to meet the needs of the population.",
                    ParentFieldOfEducationId = 143
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 146,
                    FieldCode = "040105",
                    Name = "Landscape Architecture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Landscape Architecture is the study of planning, designing and installing exterior environments with concern for functionality, practicality and human needs and requirements. It includes the design of parks and gardens integrated with the built environment.",
                    ParentFieldOfEducationId = 143
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 147,
                    FieldCode = "040107",
                    Name = "Interior and Environmental Design",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Interior and Environmental Design is the study of planning and designing interior environments of homes, offices, factories and other buildings with concern for functionality, practicality and human needs and requirements.",
                    ParentFieldOfEducationId = 143
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 148,
                    FieldCode = "040199",
                    Name = "Architecture and Urban Environment, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Architecture and Urban Environment not elsewhere classified.",
                    ParentFieldOfEducationId = 143
                },
                #endregion
                #region Building
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 149,
                    FieldCode = "0403",
                    Name = "Building",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Building is the study of the science, technology and techniques of assembling, erecting and maintaining public, commercial, industrial and residential structures and their fittings.",
                    ParentFieldOfEducationId = 142
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 150,
                    FieldCode = "040301",
                    Name = "Building Science and Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Building Science and Technology is the study of the theories and techniques required to construct and maintain structures.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 151,
                    FieldCode = "040303",
                    Name = "Building Construction Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Building Construction Management is the study of planning and controlling building projects.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 152,
                    FieldCode = "040305",
                    Name = "Building Surveying",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Building Surveying is the study of assessing the condition of buildings to ensure compliance with plans, specification and regulations and that proper techniques and materials have been used.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 153,
                    FieldCode = "040307",
                    Name = "Building Construction Economics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Building Construction Economics is the study of estimating quantities of materials and total costs involved in building and construction.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 154,
                    FieldCode = "040309",
                    Name = "Bricklaying and Stonemasonry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Bricklaying and Stonemasonry is the study of cutting, shaping, laying and joining bricks, stone and building blocks to construct and repair all types of masonry structures.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 155,
                    FieldCode = "040311",
                    Name = "Carpentry and Joinery",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Carpentry and Joinery is the study of fabricating, assembling, installing, renovating and repairing doors, frames, formwork and other fittings in buildings.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 156,
                    FieldCode = "040313",
                    Name = "Ceiling, Wall and Floor Fixing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Ceiling, Wall and Floor Fixing is the study of fixing tiles and lining materials to ceilings, walls and floors. It includes fixing cladding materials to external surfaces.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 157,
                    FieldCode = "040315",
                    Name = "Roof Fixing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Roof Fixing is the study of covering structures with roof tiles, shingles and other materials to form a waterproof surface.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 158,
                    FieldCode = "040317",
                    Name = "Plastering",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Plastering is the study of fixing fibrous sheets and applying plaster, cement-based and similar materials to ceilings, interior and exterior walls of buildings and the study of making plasterboard and plaster mouldings.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 159,
                    FieldCode = "040319",
                    Name = "Furnishing Installation",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Furnishing Installation is the study of measuring and installing curtains, blinds, awnings and other soft furnishings.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 160,
                    FieldCode = "040321",
                    Name = "Floor Coverings",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Floor Coverings is the study of measuring and laying parquetry, carpet, cork, linoleum, vinyl and other resilient floor coverings.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 161,
                    FieldCode = "040323",
                    Name = "Glazing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Glazing is the study of installing and maintaining glass panes, mirrors and windscreens.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 162,
                    FieldCode = "040325",
                    Name = "Painting, Decorating and Sign Writing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Painting, Decorating and Sign Writing is the study of applying paint, varnish and paper finishes to decorate and protect interior and exterior surfaces. It includes the study of designing and painting signs.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 163,
                    FieldCode = "040327",
                    Name = "Plumbing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Plumbing is the study of designing, installing, maintaining and repairing pipelines, fixtures, fittings and related equipment for water, steam, gas, sewage and other liquids in residential, public commercial and industrial establishments.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 164,
                    FieldCode = "040329",
                    Name = "Scaffolding and Rigging",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Scaffolding and Rigging is the study of erecting and dismantling modular scaffolding and rigging.",
                    ParentFieldOfEducationId = 149
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 165,
                    FieldCode = "040399",
                    Name = "Building, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Building not elsewhere classified.",
                    ParentFieldOfEducationId = 149
                },
                #endregion
                #endregion
                #region Agriculture, Environmental and Related Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 166,
                    FieldCode = "05",
                    Name = "Agriculture, Environmental and Related Studies",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Agriculture, Environmental and Related Studies is the study of the theory and practice of breeding, growing, gathering, reproducing and caring for plants and animals. It includes the study of the interaction between people and the environment and the application of scientific principles to the environment to protect it from deterioration."
                },
                #region Agriculture
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 167,
                    FieldCode = "0501",
                    Name = "Agriculture",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Agriculture is the study of growing, maintaining and harvesting non-intensively managed crops and pastures, and breeding, grazing and managing animals. It includes the study of farming and producing unprocessed plant and animal products.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 168,
                    FieldCode = "050101",
                    Name = "Agricultural Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Agricultural Science is the study of the non-intensive farming of animals and plants.",
                    ParentFieldOfEducationId = 167
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 169,
                    FieldCode = "050103",
                    Name = "Wool Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Wool Science is the study of producing, handling and classing wool and other fleece.",
                    ParentFieldOfEducationId = 167
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 170,
                    FieldCode = "050105",
                    Name = "Animal Husbandry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Animal Husbandry is the study of the intensive management of animals, their training, handling, care, breeding and rearing.",
                    ParentFieldOfEducationId = 167
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 171,
                    FieldCode = "050199",
                    Name = "Agriculture, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Agriculture not elsewhere classified.",
                    ParentFieldOfEducationId = 167
                },
                #endregion
                #region Horticulture and Viticulture
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 172,
                    FieldCode = "0503",
                    Name = "Horticulture and Viticulture",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Horticulture and Viticulture is the study of cultivating, propagating and producing intensively managed crops such as grapes and other fruits, vegetables, flowers, trees, shrubs and plants.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 173,
                    FieldCode = "050301",
                    Name = "Horticulture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Horticulture is the study of cultivating, propagating and producing intensively managed crops such as fruits (except grapes), vegetables, flowers, foliage, trees, shrubs and plants.",
                    ParentFieldOfEducationId = 172
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 174,
                    FieldCode = "050303",
                    Name = "Viticulture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Viticulture is the study of cultivating and managing grapevines.",
                    ParentFieldOfEducationId = 172
                },
                #endregion
                #region Forestry Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 175,
                    FieldCode = "0505",
                    Name = "Forestry Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Forestry Studies is the study of establishing, cultivating, harvesting and managing forests.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 176,
                    FieldCode = "050501",
                    Name = "Forestry Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Forestry Studies is the study of establishing, cultivating, harvesting and managing forests.",
                    ParentFieldOfEducationId = 175
                },
                #endregion
                #region Fisheries Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 177,
                    FieldCode = "0507",
                    Name = "Fisheries Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Fisheries Studies is the study of breeding, rearing, harvesting, handling, and managing fish and other aquatic resources.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 178,
                    FieldCode = "050701",
                    Name = "Aquaculture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Aquaculture is the study of breeding, rearing, harvesting, handling and processing fish and other aquatic resources.",
                    ParentFieldOfEducationId = 177
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 179,
                    FieldCode = "050799",
                    Name = "Fisheries Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Fisheries Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 177
                },
                #endregion
                #region Environmental Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 180,
                    FieldCode = "0509",
                    Name = "Environmental Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Environmental Studies is the study of the relationships between living organisms and the natural, rural, industrial and urban environments. It includes the study of the impact humans have upon the natural environment.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 181,
                    FieldCode = "050901",
                    Name = "Land, Parks and Wildlife Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Land, Parks and Wildlife Management is the study of managing land, parkland, marine and coastal zone parks, and wildlife.",
                    ParentFieldOfEducationId = 180
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 182,
                    FieldCode = "050999",
                    Name = "Environmental Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Environmental Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 180
                },
                #endregion
                #region Other Agriculture, Environmental and Related Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 183,
                    FieldCode = "0599",
                    Name = "Other Agriculture, Environmental and Related Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Agriculture, Environmental and Related Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 166
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 184,
                    FieldCode = "050901",
                    Name = "Pest and Weed Control",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Pest and Weed Control is the study of controlling noxious plants, animals, insects and fungi.",
                    ParentFieldOfEducationId = 183
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 185,
                    FieldCode = "050999",
                    Name = "Agriculture, Environmental and Related Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Agriculture, Environmental and Related Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 183
                },
                #endregion
                #endregion
                #region Health
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 186,
                    FieldCode = "06",
                    Name = "Health",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Health is the study of maintaining and restoring the physical and mental wellbeing of humans and animals."
                },
                #region Medical Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 187,
                    FieldCode = "0601",
                    Name = "Medical Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Medical Studies",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 188,
                    FieldCode = "060101",
                    Name = "General Medicine",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "General Medicine is the study of the clinical presentation and treatment of diseases.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 189,
                    FieldCode = "060103",
                    Name = "Surgery",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Surgery is the study of the medical specialisation concerned with principles and practices for treating diseases, injuries, defects and deformities by manual operation and manipulation, and by using instruments and appliances.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 190,
                    FieldCode = "060105",
                    Name = "Psychiatry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Psychiatry is the study of the medical specialisation concerned with diagnosing, preventing and treating diseases and disorders of the mind.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 191,
                    FieldCode = "060107",
                    Name = "Obstetrics and Gynaecology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Obstetrics and Gynaecology is the study of the medical specialisation concerned with the care of women during pregnancy, labour and after childbirth, and the prevention, diagnosis and treatment of diseases of the female reproductive system.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 192,
                    FieldCode = "060109",
                    Name = "Paediatrics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Paediatrics is the study of the medical specialisation concerned with normal physical growth and development from birth through to late adolescence. It includes preventing, diagnosing and treating diseases and uncommon disorders in children and adolescents.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 193,
                    FieldCode = "060111",
                    Name = "Anaesthesiology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Anaesthesiology is the study of the medical specialisation concerned with administering drugs and other substances to achieve lack of sensation with or without loss of consciousness.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 194,
                    FieldCode = "060113",
                    Name = "Pathology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Pathology is the study of the medical specialisation concerned with the causes and effects of diseases, including the structural and functional changes of body organs, tissues and fluids, and the systematic methods of detecting these changes.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 195,
                    FieldCode = "060115",
                    Name = "Radiology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Radiology is the study of the medical specialisation concerned with using radioactive substances, X-rays and other ionising radiation to diagnose, treat and follow the course of disease and its response to treatment.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 196,
                    FieldCode = "060117",
                    Name = "Internal Medicine",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Internal Medicine is the study of the medical specialisation concerned with the diagnosis and non-surgical treatment of diseases and disorders of specific bodily structures and functions.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 197,
                    FieldCode = "060119",
                    Name = "General Practice",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "General Practice is the study of providing primary and continuing medical care to patients in a community setting. It is particularly concerned with providing family and community oriented health care.",
                    ParentFieldOfEducationId = 187
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 198,
                    FieldCode = "060199",
                    Name = "Medical Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Medical Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 187
                },
                #endregion
                #region Nursing
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 199,
                    FieldCode = "0603",
                    Name = "Nursing",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Nursing is the study of the principles and practices of providing general and specialised preventative, curative, rehabilitative and palliative care to individuals and groups. It includes the study of the structure and function of the human body and mind, the restoration and maintenance of health, pain control, human behaviour and nursing ethics.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 200,
                    FieldCode = "060301",
                    Name = "General Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "General Nursing is the study of the principles and practices of providing preventative, curative and rehabilitative care to individuals and groups.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 201,
                    FieldCode = "060303",
                    Name = "Midwifery",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Midwifery is the study of the principles and practices of providing care for women during pregnancy, birthing and after childbirth and for caring for the new-born. It includes assisting parents and families during childbearing.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 202,
                    FieldCode = "060305",
                    Name = "Mental Health Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mental Health Nursing is the study of the nursing specialisation concerned with caring for persons with mental health problems.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 203,
                    FieldCode = "060307",
                    Name = "Community Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Community Nursing is the study of the nursing specialisation concerned with caring for individuals and groups in the community.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 204,
                    FieldCode = "060309",
                    Name = "Critical Care Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Critical Care Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for patients requiring intensive therapy.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 205,
                    FieldCode = "060311",
                    Name = "Aged Care Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Aged Care Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for elderly patients.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 206,
                    FieldCode = "060313",
                    Name = "Palliative Care Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Palliative Care Nursing is the study of the nursing specialisation concerned with caring for the terminally ill and their families.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 207,
                    FieldCode = "060315",
                    Name = "Mothercraft Nursing and Family and Child Health Nursing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Mothercraft Nursing and Family and Child Health Nursing is the study of the nursing specialisation concerned with the theory and practice of caring for young children and their families.",
                    ParentFieldOfEducationId = 199
                },
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 208,
                    FieldCode = "060399",
                    Name = "Nursing, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Nursing not elsewhere classified.",
                    ParentFieldOfEducationId = 199
                },
                #endregion
                #region Pharmacy
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 209,
                    FieldCode = "0605",
                    Name = "Pharmacy",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Pharmacy is the study of the preparation and dispensing of drugs.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 210,
                    FieldCode = "060501",
                    Name = "Pharmacy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Pharmacy is the study of the preparation and dispensing of drugs.",
                    ParentFieldOfEducationId = 209
                },
                #endregion
                #region Dental Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 211,
                    FieldCode = "0607",
                    Name = "Dental Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Dental Studies is the study of diagnosing, treating and preventing diseases and abnormalities of the teeth and adjacent tissues. It includes the study of designing, making and repairing dental prostheses and orthodontic appliances, and assisting with dental procedures.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 212,
                    FieldCode = "060701",
                    Name = "Dentistry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Dentistry is the study of diagnosing, treating and preventing diseases of the teeth and adjacent tissues. It includes correcting malocclusion and restoring and replacing missing dental and oral structures.",
                    ParentFieldOfEducationId = 211
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 213,
                    FieldCode = "060703",
                    Name = "Dental Assisting",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Dental Assisting is the study of providing assistance to dentists in clinical settings.",
                    ParentFieldOfEducationId = 211
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 214,
                    FieldCode = "060705",
                    Name = "Dental Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Dental Technology is the study of designing, making and repairing dental prostheses and orthodontic appliances.",
                    ParentFieldOfEducationId = 211
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 215,
                    FieldCode = "060799",
                    Name = "Dental Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Dental Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 211
                },
                #endregion
                #region Optical Science
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 216,
                    FieldCode = "0609",
                    Name = "Optical Science",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Optical Science is the study of measuring and assessing vision, and prescribing, preparing and dispensing corrective lenses.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 217,
                    FieldCode = "060901",
                    Name = "Optometry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Optometry is the study of measuring and assessing vision, and prescribing lenses for visual correction.",
                    ParentFieldOfEducationId = 216
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 218,
                    FieldCode = "060903",
                    Name = "Optical Technology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Optical Technology is the study of preparing and dispensing corrective lenses according to prescription.",
                    ParentFieldOfEducationId = 216
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 219,
                    FieldCode = "060999",
                    Name = "Optical Science, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Optical Science not elsewhere classified.",
                    ParentFieldOfEducationId = 216
                },
                #endregion
                #region Veterinary Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 220,
                    FieldCode = "0611",
                    Name = "Veterinary Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Veterinary Studies is the study of identifying, preventing and treating disease and injury in animals, and their general veterinary care.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 221,
                    FieldCode = "061101",
                    Name = "Veterinary Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Veterinary Science is the study of diagnosing and treating animal diseases, ailments and injuries, and preventing and containing the spread of animal diseases.",
                    ParentFieldOfEducationId = 220
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 222,
                    FieldCode = "061103",
                    Name = "Veterinary Assisting",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Veterinary Assisting is the study of caring for sick, injured and infirm animals undergoing treatment in veterinary clinics.",
                    ParentFieldOfEducationId = 220
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 223,
                    FieldCode = "061199",
                    Name = "Veterinary Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Veterinary Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 220
                },
                #endregion
                #region Public Health
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 224,
                    FieldCode = "0613",
                    Name = "Public Health",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Public Health is the study of the principles and practices of protecting, promoting, maintaining and restoring the health of the community.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 225,
                    FieldCode = "061301",
                    Name = "Occupational Health and Safety",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Occupational Health and Safety is the study of recognising, evaluating and controlling environmental factors associated with the interaction of individuals and the workplace.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 226,
                    FieldCode = "061303",
                    Name = "Environmental Health",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Environmental Health is the study of recognising, evaluating and controlling environmental factors affecting public health.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 227,
                    FieldCode = "061305",
                    Name = "Indigenous Health",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Indigenous Health is the study of the health of the indigenous population within the broader context of socio-economic development of Aboriginal and Torres Strait Islander communities.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 228,
                    FieldCode = "061307",
                    Name = "Health Promotion",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Health Promotion is the study of promoting a healthy lifestyle and influencing behaviour to improve health.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 229,
                    FieldCode = "061309",
                    Name = "Community Health",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Community Health is the study of health practices in the community which support and assist the management of disabilities and illness.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 230,
                    FieldCode = "061311",
                    Name = "Epidemiology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Epidemiology is the study of the incidence, distribution and possible control of infectious and chronic diseases as they effect groups of people.",
                    ParentFieldOfEducationId = 224
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 231,
                    FieldCode = "061399",
                    Name = "Public Health, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Public Health not elsewhere classified.",
                    ParentFieldOfEducationId = 224
                },
                #endregion
                #region Radiography
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 232,
                    FieldCode = "0615",
                    Name = "Radiography",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Radiography is the study of technologies which use ionising and non-ionising (e.g. ultrasound) radiation for producing diagnostic images and administering radiation therapy.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 233,
                    FieldCode = "061501",
                    Name = "Radiography",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Radiography is the study of technologies which use ionising and non-ionising (e.g. ultrasound) radiation for producing diagnostic images and administering radiation therapy.",
                    ParentFieldOfEducationId = 232
                },
                #endregion
                #region Rehabilitation Therapies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 234,
                    FieldCode = "0617",
                    Name = "Rehabilitation Therapies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Rehabilitation Therapies is the study of the practices and principles that restore a person to an optimal quality of life. It includes treating individuals so that they can return to normal duties after being affected by accident or disease.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 235,
                    FieldCode = "061701",
                    Name = "Physiotherapy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Physiotherapy is the study of assessing people with temporary and longer term physical injuries and movement disorders, and restoring maximum movement and functional ability.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 236,
                    FieldCode = "061703",
                    Name = "Occupational Therapy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Occupational Therapy is the study of treating physical, cognitive and psychiatric conditions through activities in order to optimise functioning and independence in daily life.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 237,
                    FieldCode = "061705",
                    Name = "Chiropractic and Osteopathy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Chiropractic and Osteopathy is the study of assessing and relieving disorders of the body through manipulating and treating the musculoskeletal system.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 238,
                    FieldCode = "061707",
                    Name = "Speech Pathology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Speech Pathology is the study of assessing and treating speech and language disorders.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 239,
                    FieldCode = "061709",
                    Name = "Audiology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Audiology is the study of assessing and treating hearing disorders.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 240,
                    FieldCode = "061711",
                    Name = "Massage Therapy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Massage Therapy is the study of treating disorders through massage of the soft tissue.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 241,
                    FieldCode = "061713",
                    Name = "Podiatry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Podiatry is the study of assessing and treating physical ailments of the human foot and lower limb.",
                    ParentFieldOfEducationId = 234
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 242,
                    FieldCode = "061799",
                    Name = "Rehabilitation Therapies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Rehabilitation Therapies not elsewhere classified.",
                    ParentFieldOfEducationId = 234
                },
                #endregion
                #region Complementary Therapies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 243,
                    FieldCode = "0619",
                    Name = "Complementary Therapies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Complementary Therapies is the study of the prevention and treatment of health problems using natural and traditional remedies.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 244,
                    FieldCode = "061901",
                    Name = "Naturopathy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Naturopathy is the study of treating diseases using natural therapies.",
                    ParentFieldOfEducationId = 243
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 245,
                    FieldCode = "061903",
                    Name = "Acupuncture",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Acupuncture is the study of treating diseases through influencing points on meridians using fine needles.",
                    ParentFieldOfEducationId = 243
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 246,
                    FieldCode = "061905",
                    Name = "Traditional Chinese Medicine",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Traditional Chinese Medicine is the study of treating diseases through traditional Chinese therapies.",
                    ParentFieldOfEducationId = 243
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 247,
                    FieldCode = "061999",
                    Name = "Complementary Therapies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Complementary Therapies not elsewhere classified.",
                    ParentFieldOfEducationId = 243
                },
                #endregion
                #region Other Health
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 248,
                    FieldCode = "0699",
                    Name = "Other Health",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Health not elsewhere classified.",
                    ParentFieldOfEducationId = 186
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 249,
                    FieldCode = "069901",
                    Name = "Nutrition and Dietetics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Nutrition and Dietetics is the study of the nutritional and dietary needs of humans.",
                    ParentFieldOfEducationId = 248
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 250,
                    FieldCode = "069903",
                    Name = "Human Movement",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Human Movement is the study of the nature, cause and control of movement. It includes posture and balance, and the science of human performance.",
                    ParentFieldOfEducationId = 248
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 251,
                    FieldCode = "069905",
                    Name = "Paramedical Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Paramedical Studies is the study of the emergency treatment of the sick and injured prior to and during transfer to a hospital or medical facility.",
                    ParentFieldOfEducationId = 248
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 252,
                    FieldCode = "069907",
                    Name = "First Aid",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "First Aid is the study of skills necessary to aid the ill and injured until medical aid arrives.",
                    ParentFieldOfEducationId = 248
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 253,
                    FieldCode = "069999",
                    Name = "Health, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Health not elsewhere classified.",
                    ParentFieldOfEducationId = 248
                },
                #endregion
                #endregion
                #region Education
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 254,
                    FieldCode = "07",
                    Name = "Education",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Education is the study of the process of learning. It includes the theories, methods and techniques of imparting knowledge and skills to others."
                },
                #region Teacher Education
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 255,
                    FieldCode = "0701",
                    Name = "Teacher Education",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Teacher Education is the study of teaching and learning methods and their application to educating people.",
                    ParentFieldOfEducationId = 254
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 256,
                    FieldCode = "070101",
                    Name = "Teacher Education: Early Childhood",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Early Childhood is the study of the theories, methods and practice of teaching children from birth to 8 years of age within formal education settings.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 257,
                    FieldCode = "070103",
                    Name = "Teacher Education: Primary",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Primary is the study of the theories, methods and practice of teaching children between the ages of 5 and 12 within formal school settings.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 258,
                    FieldCode = "070105",
                    Name = "Teacher Education: Secondary",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Secondary is the study of the theories, methods and practice of teaching secondary school students between the ages of 12 and 18 within formal school and college settings.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 259,
                    FieldCode = "070107",
                    Name = "Teacher-Librarianship",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher-Librarianship is the study of the theories, methods and practice of teaching relating to the integration of library and information resources in schools and colleges.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 260,
                    FieldCode = "070109",
                    Name = "Teacher Education: Vocational Education and Training",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Vocational Education and Training is the study of the theories, methods and practice of teaching trade and other vocational subjects.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 261,
                    FieldCode = "070111",
                    Name = "Teacher Education: Higher Education",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Higher Education is the study of the theories, methods and practice of teaching in higher educational institutions.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 262,
                    FieldCode = "070113",
                    Name = "Teacher Education: Special Education",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Teacher Education: Special Education is the study of the theories, methods and practice of teaching children with special learning needs, including children with physical and mental disabilities and impairments, and gifted children. Special Education may be conducted in special schools or by providing support for teachers in primary and secondary schools.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 263,
                    FieldCode = "070115",
                    Name = "English as a Second Language Teaching",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "English as a Second Language Teaching is the study of theories, methods and practice of teaching English to those whose first language is other than English, including teaching children in school settings and teaching adults and children in other settings.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 264,
                    FieldCode = "070117",
                    Name = "Nursing Education Teacher Training",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Nursing Education Teacher Training is the study of the theories, methods and practice of teaching nurses in tertiary educational institutions and hospital-based settings.",
                    ParentFieldOfEducationId = 255
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 265,
                    FieldCode = "070199",
                    Name = "Teacher Education, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Teacher Education not elsewhere classified.",
                    ParentFieldOfEducationId = 255
                },
                #endregion
                #region Curriculum and Education Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 266,
                    FieldCode = "0703",
                    Name = "Curriculum and Education Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Curriculum and Education Studies is the study of developing and evaluating appropriate curricula and teaching strategies and practices.",
                    ParentFieldOfEducationId = 254
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 267,
                    FieldCode = "070301",
                    Name = "Curriculum Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Curriculum Studies is the study of developing and evaluating appropriate curricula for different Key Learning Areas and subjects to teach to particular groups of children and adults.",
                    ParentFieldOfEducationId = 266
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 268,
                    FieldCode = "070303",
                    Name = "Education Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Education Studies is the study of the theoretical background of traditional and current teaching practices.",
                    ParentFieldOfEducationId = 266
                },
                #endregion
                #region Other Education
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 269,
                    FieldCode = "0799",
                    Name = "Other Education",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Education not elsewhere classified.",
                    ParentFieldOfEducationId = 254
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 270,
                    FieldCode = "079999",
                    Name = "Education, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This narrow field includes all Education not elsewhere classified.",
                    ParentFieldOfEducationId = 269
                },
                #endregion
                #endregion
                #region Management and Commerce
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 271,
                    FieldCode = "08",
                    Name = "Management and Commerce",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Management and Commerce is the study of the theory and practice of planning, directing, organising, motivating and co-ordinating the human and material resources of private and public organisations and institutions. It includes the merchandising and provision of goods and services and personal development."
                },
                #region Accounting
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 272,
                    FieldCode = "0801",
                    Name = "Accounting",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Accounting is the study of the theory and practice of developing, maintaining, auditing and analysing financial records.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 273,
                    FieldCode = "080101",
                    Name = "Accounting",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Accounting is the study of the theory and practice of developing, maintaining, auditing and analysing financial records.",
                    ParentFieldOfEducationId = 272
                },
                #endregion
                #region Business and Management
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 274,
                    FieldCode = "0803",
                    Name = "Business Management",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Business Management is the study of planning and directing the functions and activities of persons, organisations and institutions.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 275,
                    FieldCode = "080301",
                    Name = "Business Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Business Management is the study of planning and directing the activities of commercial enterprises. It includes the study of the nature, operation and role of business, and the resolution of management and administrative problems.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 276,
                    FieldCode = "080303",
                    Name = "Human Resource Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Human Resource Management is the study of staffing policy, practice and management within an organisation.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 277,
                    FieldCode = "080305",
                    Name = "Personal Management Training",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Personal Management Training is the study of self-improvement techniques.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 278,
                    FieldCode = "080307",
                    Name = "Organisation Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Organisation Management is the study of organisational structure and change.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 279,
                    FieldCode = "080309",
                    Name = "Industrial Relations",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Industrial Relations is the study of the relationship between employers and employees, and the application of such relations to workplace issues.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 280,
                    FieldCode = "080311",
                    Name = "International Business",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "International Business is the study of international trade, import and export processes and regulations and customs procedures and regulations.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 281,
                    FieldCode = "080313",
                    Name = "Public and Health Care Administration",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Public and Health Care Administration is the study of planning and directing the functions and operations of organisations whose primary objective is the provision of services for the public good.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 282,
                    FieldCode = "080315",
                    Name = "Project Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Project Management is the study of planning and managing a total project process.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 283,
                    FieldCode = "080317",
                    Name = "Quality Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Quality Management is the study of initiating and implementing quality assurance techniques and procedures to meet standards of best practice.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 284,
                    FieldCode = "080319",
                    Name = "Hospitality Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Hospitality Management is the study of managing the operations of organisations which provide hospitality services. It includes conference and special events management.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 285,
                    FieldCode = "080321",
                    Name = "Farm Management and Agribusiness",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Farm Management and Agribusiness is the study of managing farming and agricultural business enterprises.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 286,
                    FieldCode = "080323",
                    Name = "Tourism Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Tourism Management is the study of planning and managing the activities of tourism focused enterprises.",
                    ParentFieldOfEducationId = 274
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 287,
                    FieldCode = "080399",
                    Name = "Business and Management, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Business and Management not elsewhere classified.",
                    ParentFieldOfEducationId = 274
                },
                #endregion
                #region Sales and Marketing
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 288,
                    FieldCode = "0805",
                    Name = "Sales and Marketing",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Sales and Marketing is the study of identifying and developing markets, and promoting and selling goods, services and properties.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 289,
                    FieldCode = "080501",
                    Name = "Sales",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Sales is the study of buying and selling goods and services.",
                    ParentFieldOfEducationId = 288
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 290,
                    FieldCode = "080503",
                    Name = "Real Estate",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Real Estate is the study of developing, purchasing, leasing and selling buildings, businesses and properties.",
                    ParentFieldOfEducationId = 288
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 291,
                    FieldCode = "080505",
                    Name = "Marketing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Marketing is the study of identifying market opportunities and developing and implementing strategies for pricing and promoting products and services.",
                    ParentFieldOfEducationId = 288
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 292,
                    FieldCode = "080507",
                    Name = "Advertising",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Advertising is the study of informing potential customers of the nature of products and services and their merits.",
                    ParentFieldOfEducationId = 288
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 293,
                    FieldCode = "080509",
                    Name = "Public Relations",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Public Relations is the study of creating and maintaining an understanding and a favourable view of an organisation and its products, services and role.",
                    ParentFieldOfEducationId = 288
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 294,
                    FieldCode = "080599",
                    Name = "Sales and Marketing, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Sales and Marketing not elsewhere classified.",
                    ParentFieldOfEducationId = 288
                },
                #endregion
                #region Tourism
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 295,
                    FieldCode = "0807",
                    Name = "Tourism",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Tourism is the study of the structure and operations of the tourism industry, tour guiding, and ticketing and reservation practices.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 296,
                    FieldCode = "080701",
                    Name = "Tourism",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Tourism is the study of the structure and operations of the tourism industry, tour guiding, and ticketing and reservation practices.",
                    ParentFieldOfEducationId = 295
                },
                #endregion
                #region Office Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 297,
                    FieldCode = "0809",
                    Name = "Office Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Office Studies is the study of planning, organising and managing office systems, and operating office equipment. It includes the study of clerical skills.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 298,
                    FieldCode = "080901",
                    Name = "Secretarial and Clerical Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Secretarial and Clerical Studies is the study of shorthand, record keeping, correspondence and general office procedures.",
                    ParentFieldOfEducationId = 297
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 299,
                    FieldCode = "080903",
                    Name = "Keyboard Skills",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Keyboard Skills is the study of typing and data entry.",
                    ParentFieldOfEducationId = 297
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 300,
                    FieldCode = "080905",
                    Name = "Practical Computing Skills",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Practical Computing Skills is the study of basic computer operation and using software packages.",
                    ParentFieldOfEducationId = 297
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 301,
                    FieldCode = "080999",
                    Name = "Office Studies, n.e.c",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Office Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 297
                },
                #endregion
                #region Banking, Finance and Related Fields
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 302,
                    FieldCode = "0811",
                    Name = "Banking, Finance and Related Fields",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Banking, Finance and Related Fields is the study of planning, directing, organising and controlling financial activities and services. It includes the provision of insurance and investment services at the corporate and individual level.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 303,
                    FieldCode = "081101",
                    Name = "Banking and Finance",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Banking and Finance is the study of directing, planning and providing financial institution services in relation to savings and loans.",
                    ParentFieldOfEducationId = 302
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 304,
                    FieldCode = "081103",
                    Name = "Insurance and Actuarial Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Insurance and Actuarial Studies is the study of directing, planning and providing insurance services, and applying mathematical and statistical analysis to financial planning problems.",
                    ParentFieldOfEducationId = 302
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 305,
                    FieldCode = "081105",
                    Name = "Investment and Securities",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Investment and Securities is the study of directing, planning and providing investment and securities services.",
                    ParentFieldOfEducationId = 302
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 306,
                    FieldCode = "081199",
                    Name = "Banking, Finance and Related Fields, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Banking, Finance and Related Fields not elsewhere classified.",
                    ParentFieldOfEducationId = 302
                },
                #endregion
                #region Other Management and Commerce
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 307,
                    FieldCode = "0899",
                    Name = "Other Management and Commerce",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Management and Commerce not elsewhere classified.",
                    ParentFieldOfEducationId = 271
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 308,
                    FieldCode = "089901",
                    Name = "Purchasing, Warehousing and Distribution",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Purchasing, Warehousing and Distribution is the study of purchasing, supplying, storing and despatching goods and other materials.",
                    ParentFieldOfEducationId = 307
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 309,
                    FieldCode = "089903",
                    Name = "Valuation",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Valuation is the study of valuing land, buildings, businesses, properties, machinery, art and personal items.",
                    ParentFieldOfEducationId = 307
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 310,
                    FieldCode = "089999",
                    Name = "Management and Commerce, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Management and Commerce not elsewhere classified.",
                    ParentFieldOfEducationId = 307
                },
                #endregion
                #endregion
                #region Society and Culture
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 311,
                    FieldCode = "09",
                    Name = "Society and Culture",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Society and Culture is the study of the physical, social and cultural organisation of human society and their influence on the individual and groups."
                },
                #region Political Science and Policy Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 312,
                    FieldCode = "0901",
                    Name = "Political Science and Policy Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Political Science and Policy Studies is the study of political theories, processes, events and institutions, the operation and function of government, and the development and analysis of public and other policies.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 313,
                    FieldCode = "090101",
                    Name = "Political Science",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Political Science is the study of political ideas, processes, institutions and behaviour, and how they influence public decisions within and among communities.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 314,
                    FieldCode = "090103",
                    Name = "Policy Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Policy Studies is the study of analysing and developing policies to achieve societal and corporate goals.",
                    ParentFieldOfEducationId = 311
                },
                #endregion
                #region Studies in Human Society
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 315,
                    FieldCode = "0903",
                    Name = "Studies in Human Society",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Studies in Human Society is the study of the past and present behaviour of humans and their interaction, social organisation and geographical distribution.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 316,
                    FieldCode = "090301",
                    Name = "Sociology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Sociology is the study of the origin, development, organisation and functioning of human society, including the study of the patterns of social relations.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 317,
                    FieldCode = "090303",
                    Name = "Anthropology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Anthropology is the study of the diversity of human cultural practices and beliefs by participant observation and comparison.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 318,
                    FieldCode = "090305",
                    Name = "History",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "History is the study of past cultures, events and ideas using written documents and other evidence.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 319,
                    FieldCode = "090307",
                    Name = "Archaeology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Archaeology is the study of human history and prehistory through the excavation of sites and the analysis of artefacts and other physical remains.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 320,
                    FieldCode = "090309",
                    Name = "Human Geography",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Human Geography is the study of spatial variations between human populations and the interactions between people and their environment.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 321,
                    FieldCode = "090311",
                    Name = "Indigenous Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Indigenous Studies is the study of indigenous culture and societies and their relationship to the broader society of a particular country.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 322,
                    FieldCode = "090313",
                    Name = "Gender Specific Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Gender Specific Studies is the study of the ways that people and society develop various ideas and beliefs concerning the roles and functions of a specific gender and the way that this gender relates to society.",
                    ParentFieldOfEducationId = 315
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 323,
                    FieldCode = "090399",
                    Name = "Studies in Human Society, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Studies in Human Society not elsewhere classified.",
                    ParentFieldOfEducationId = 315
                },
                #endregion
                #region Human Welfare Studies and Services
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 324,
                    FieldCode = "0905",
                    Name = "Human Welfare Studies and Services",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Human Welfare Studies and Services is the study of social intervention designed to help persons, individually and collectively, maximise their social and economic wellbeing.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 325,
                    FieldCode = "090501",
                    Name = "Social Work",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Social Work is the study of promoting, restoring, maintaining and enhancing the functioning of individuals, families, social groups, organisations and communities by the utilisation of resources within individuals and the social environment in order to alleviate social problems.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 326,
                    FieldCode = "090503",
                    Name = "Children’s Services",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Children’s Services is the study of the care and development of children.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 327,
                    FieldCode = "090505",
                    Name = "Youth Work",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Youth Work is the study of the development and support of youth.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 328,
                    FieldCode = "090507",
                    Name = "Care for the Aged",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Care for the Aged is the study of caring for elderly people in various social support services associated with families and groups in the community.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 329,
                    FieldCode = "090509",
                    Name = "Care for the Disabled",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Care for the Disabled is the study of caring for disabled people in various social support services associated with families and groups in the community.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 330,
                    FieldCode = "090511",
                    Name = "Residential Client Care",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Residential Client Care is the study of caring for people living in a variety of welfare and residential settings.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 331,
                    FieldCode = "090513",
                    Name = "Counselling",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Counselling is the study of the practice and skills required to provide guidance on personal, social and psychological problems.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 332,
                    FieldCode = "090515",
                    Name = "Welfare Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Welfare Studies is the study of providing personal services to individuals and family groups who are socially disadvantaged or otherwise in need.",
                    ParentFieldOfEducationId = 324
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 333,
                    FieldCode = "090599",
                    Name = "Human Welfare Studies and Services, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Human Welfare Studies and Services not elsewhere classified.",
                    ParentFieldOfEducationId = 324
                },
                #endregion
                #region Behavioural Science
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 334,
                    FieldCode = "0907",
                    Name = "Behavioural Science",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Behavioural Science is the study of the causes of behaviour as a result of individual differences, experience and environment, and the modification of that behaviour.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 335,
                    FieldCode = "090701",
                    Name = "Psychology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Psychology is the study of the science of human nature and of mental states and processes. It includes the study of human and animal nature.",
                    ParentFieldOfEducationId = 334
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 336,
                    FieldCode = "090799",
                    Name = "Behavioural Science, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Behavioural Science not elsewhere classified.",
                    ParentFieldOfEducationId = 334
                },
                #endregion
                #region Law
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 337,
                    FieldCode = "0909",
                    Name = "Law",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Law is the study of the principles and regulations which are recognised in the form of legislation or customs and policies recognised by judicial decision.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 338,
                    FieldCode = "090901",
                    Name = "Business and Commercial Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Business and Commercial Law is the study of the principles and regulations governing business and commercial practices.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 339,
                    FieldCode = "090903",
                    Name = "Constitutional Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Constitutional Law is the study of the principles and regulations in relation to the constitution and the respective powers of the federal and state governments.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 340,
                    FieldCode = "090905",
                    Name = "Criminal Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Criminal Law is the study of the principles and practices associated with the body of law which deals with criminal offences.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 341,
                    FieldCode = "090907",
                    Name = "Family Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Family Law is the study of the principles and regulations in relation to the family.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 342,
                    FieldCode = "090909",
                    Name = "International Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "International Law is the study of the system of rules governing the conduct of international relations, and the relationship between Australian laws and the laws of other countries.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 343,
                    FieldCode = "090911",
                    Name = "Taxation Law",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Taxation Law is the study of principles and regulations related to government collection of revenues from corporate and individual tax payers.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 344,
                    FieldCode = "090913",
                    Name = "Legal Practice",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Legal Practice is the study of the issues associated with practising law. It includes the methods and principles involved in advising and representing clients on matters relating to law.",
                    ParentFieldOfEducationId = 337
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 345,
                    FieldCode = "090999",
                    Name = "Law, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Law not elsewhere classified.",
                    ParentFieldOfEducationId = 337
                },
                #endregion
                #region Justice and Law Enforcement
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 346,
                    FieldCode = "0911",
                    Name = "Justice and Law Enforcement",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Justice and Law Enforcement is the study of the principles and procedures for formally maintaining social order.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 347,
                    FieldCode = "091101",
                    Name = "Justice Administration",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Justice Administration is the study of the theory and practice of the administrative processes in the justice system.",
                    ParentFieldOfEducationId = 346
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 348,
                    FieldCode = "091103",
                    Name = "Legal Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Legal Studies is the study of the administrative and legal skills required to assist in legal practice.",
                    ParentFieldOfEducationId = 346
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 349,
                    FieldCode = "091105",
                    Name = "Police Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Police Studies is the study of the maintenance of law and order, and crime detection and prevention.",
                    ParentFieldOfEducationId = 346
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 350,
                    FieldCode = "091199",
                    Name = "Justice and Law Enforcement, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Justice and Law Enforcement not elsewhere classified.",
                    ParentFieldOfEducationId = 346
                },
                #endregion
                #region Librarianship, Information Management and Curatorial Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 351,
                    FieldCode = "0913",
                    Name = "Librarianship, Information Management and Curatorial Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Librarianship, Information Management and Curatorial Studies is the study of selecting, acquiring, organising, storing and facilitating the use of collections of information, and locating, identifying and assessing cultural heritage resources.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 352,
                    FieldCode = "091301",
                    Name = "Librarianship and Information Management",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Librarianship and Information Management is the study of selecting, acquiring, organising and storing collections of information, and facilitating the use of information.",
                    ParentFieldOfEducationId = 351
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 353,
                    FieldCode = "091303",
                    Name = "Curatorial Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Curatorial Studies is the study of locating, identifying and assessing cultural heritage resources.",
                    ParentFieldOfEducationId = 351
                },
                #endregion
                #region Language and Literature
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 354,
                    FieldCode = "0915",
                    Name = "Language and Literature",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Language and Literature is the study of the structure and use of languages, and the literature of particular times and geographic areas.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 355,
                    FieldCode = "091501",
                    Name = "English Language",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "English Language is the study of reading, writing and speaking the English language.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 356,
                    FieldCode = "091503",
                    Name = "Northern European Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Northern European Languages is the study of reading, writing and speaking the languages of Northern Europe.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 357,
                    FieldCode = "091505",
                    Name = "Southern European Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Southern European Languages is the study of reading, writing and speaking the languages of Southern Europe.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 358,
                    FieldCode = "091507",
                    Name = "Eastern European Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Eastern European Languages is the study of the reading, writing and speaking the languages of Eastern Europe.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 359,
                    FieldCode = "091509",
                    Name = "Southwest Asian and North African Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Southwest Asian and North African Languages is the study of reading, writing and speaking the languages of Southwest Asia and North Africa.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 360,
                    FieldCode = "091511",
                    Name = "Southern Asian Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Southern Asian Languages is the study of reading, writing and speaking the languages of Southern Asia.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 361,
                    FieldCode = "091513",
                    Name = "Southeast Asian Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Southeast Asian Languages is the study of reading, writing and speaking the languages of Southeast Asia.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 362,
                    FieldCode = "091515",
                    Name = "Eastern Asian Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Eastern Asian Languages is the study of reading, writing and speaking the languages of Eastern Asia.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 363,
                    FieldCode = "091517",
                    Name = "Australian Indigenous Languages",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Australian Indigenous Languages is the study of reading, writing and speaking the languages of the Australian indigenous people.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 364,
                    FieldCode = "091519",
                    Name = "Translating and Interpreting",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Translating and Interpreting is the study of translating and interpreting foreign languages into the mother tongue and vice versa.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 365,
                    FieldCode = "091521",
                    Name = "Linguistics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Linguistics is the study of the structure and composition of language.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 366,
                    FieldCode = "091523",
                    Name = "Literature",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Literature is the study of written works valued for form of writing or expression.",
                    ParentFieldOfEducationId = 354
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 367,
                    FieldCode = "091599",
                    Name = "Language and Literature, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Language and Literature not elsewhere classified.",
                    ParentFieldOfEducationId = 354
                },
                #endregion
                #region Philosophy and Religious Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 368,
                    FieldCode = "0917",
                    Name = "Philosophy and Religious Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Philosophy and Religious Studies is the study of the nature and context of human experience, perception and interpretation of reality, and human spirituality and beliefs.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 369,
                    FieldCode = "091701",
                    Name = "Philosophy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Philosophy is the study of the fundamental nature of knowledge, reality and existence.",
                    ParentFieldOfEducationId = 358
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 370,
                    FieldCode = "091703",
                    Name = "Religious Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Religious Studies is the study of a set of beliefs and practices, usually involving acknowledgement of a divine or higher being or power, by which people order the conduct of their lives both practically and in a moral sense.",
                    ParentFieldOfEducationId = 358
                },
                #endregion
                #region Economics and Econometrics
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 371,
                    FieldCode = "0919",
                    Name = "Economics and Econometrics",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Economics and Econometrics is the study of the production, consumption and transfer of wealth, and developing and analysing models which describe this behaviour.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 372,
                    FieldCode = "091901",
                    Name = "Economics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Economics is the study of the production, consumption and transfer of wealth.",
                    ParentFieldOfEducationId = 371
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 373,
                    FieldCode = "091903",
                    Name = "Econometrics",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Econometrics is the study of analysing and describing economic data using mathematical and statistical methods.",
                    ParentFieldOfEducationId = 371
                },
                #endregion
                #region Sport and Recreation
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 374,
                    FieldCode = "0921",
                    Name = "Sport and Recreation",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Sport and Recreation  is the study of sport, recreational and leisure activities, their role in society, and the development of recreational and leisure programmes.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 375,
                    FieldCode = "092101",
                    Name = "Sport and Recreation Activities",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Sport and Recreation Activities is the study of the theory and practice of participating in various sporting and recreational activities.",
                    ParentFieldOfEducationId = 374
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 376,
                    FieldCode = "092103",
                    Name = "Sports Coaching, Officiating and Instruction",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Sports Coaching, Officiating and Instruction is the study of the techniques for coaching and instructing individuals and teams in various sporting activities. It includes studying the techniques of officiating at various sporting events.",
                    ParentFieldOfEducationId = 374
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 377,
                    FieldCode = "092199",
                    Name = "Sport and Recreation, n.e.c",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Sport and Recreation not elsewhere classified.",
                    ParentFieldOfEducationId = 374
                },
                #endregion
                #region Other Society and Culture
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 378,
                    FieldCode = "0999",
                    Name = "Other Society and Culture",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Society and Culture not elsewhere classified.",
                    ParentFieldOfEducationId = 311
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 379,
                    FieldCode = "099901",
                    Name = "Family and Consumer Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Family and Consumer Studies is the study of the wellbeing of individuals and families and the way that they manage resources to achieve their goals.",
                    ParentFieldOfEducationId = 378
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 380,
                    FieldCode = "099903",
                    Name = "Criminology",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Criminology is the study of crime and issues related to offenders and victims.",
                    ParentFieldOfEducationId = 378
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 381,
                    FieldCode = "099905",
                    Name = "Security Services",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Security Services is the study of protecting properties, premises and people against intruders and damage.",
                    ParentFieldOfEducationId = 378
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 382,
                    FieldCode = "099999",
                    Name = "Society and Culture, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Society and Culture not elsewhere classified.",
                    ParentFieldOfEducationId = 378
                },
                #endregion
                #endregion
                #region Creative Arts
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 383,
                    FieldCode = "10",
                    Name = "Creative Arts",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Creative Arts is the study of creating and performing works of art, music, dance and drama. It includes the study of clothing design and creation, and communicating through a variety of media."
                },
                #region Performing Arts
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 384,
                    FieldCode = "1001",
                    Name = "Performing Arts",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Performing Arts is the study of creating and performing works of music, dance and drama.",
                    ParentFieldOfEducationId = 383
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 385,
                    FieldCode = "100101",
                    Name = "Music",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Music is the study of the history, theory, creation and performance of music.",
                    ParentFieldOfEducationId = 384
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 386,
                    FieldCode = "100103",
                    Name = "Drama and Theatre Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Drama and Theatre Studies is the study of the history, theory, creation and performance of dramatic works. It includes speech, movement, mime, characterisation, improvisation and stage craft.",
                    ParentFieldOfEducationId = 384
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 387,
                    FieldCode = "100105",
                    Name = "Dance",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Dance is the study of the history, theory, creation and performance of works of dance.",
                    ParentFieldOfEducationId = 384
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 388,
                    FieldCode = "100199",
                    Name = "Performing Arts, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Performing Arts, n.e.c.",
                    ParentFieldOfEducationId = 384
                },
                #endregion
                #region Visual Arts and Crafts
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 389,
                    FieldCode = "1003",
                    Name = "Visual Arts and Crafts",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Visual Arts and Crafts is the study of non-literary visual forms of creative expression for artistic and aesthetic purposes.",
                    ParentFieldOfEducationId = 383
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 390,
                    FieldCode = "100301",
                    Name = "Fine Arts",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Fine Arts is the study of non-literary visual forms of creative expression for artistic and aesthetic purposes, and the methods of creating those forms.",
                    ParentFieldOfEducationId = 389
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 391,
                    FieldCode = "100303",
                    Name = "Photography",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Photography is the study of composing, taking, developing, printing, enlarging and presenting photographs for creative and practical purposes.",
                    ParentFieldOfEducationId = 389
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 392,
                    FieldCode = "100305",
                    Name = "Crafts",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Crafts is the study of fashioning individual objects for decorative, ornamental and functional purpose.",
                    ParentFieldOfEducationId = 389
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 393,
                    FieldCode = "100307",
                    Name = "Jewellery Making",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Jewellery Making is the study of designing, producing and repairing ornaments for personal adornment.",
                    ParentFieldOfEducationId = 389
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 394,
                    FieldCode = "100309",
                    Name = "Floristry",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Floristry is the study of designing and displaying floral arrangements.",
                    ParentFieldOfEducationId = 389
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 395,
                    FieldCode = "100399",
                    Name = "Visual Arts and Crafts, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Visual Arts and Crafts not elsewhere classified.",
                    ParentFieldOfEducationId = 389
                },
                #endregion
                #region Graphic and Design Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 396,
                    FieldCode = "1005",
                    Name = "Graphic and Design Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Graphic and Design Studies is the study of the techniques and skills necessary to design and produce finished work, and solve design and visual communication problems. It includes the study of clothing design and creation.",
                    ParentFieldOfEducationId = 383
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 397,
                    FieldCode = "100501",
                    Name = "Graphic Arts and Design Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Graphic Arts and Design Studies is the study of designing and producing visual representations of concepts and information.",
                    ParentFieldOfEducationId = 396
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 398,
                    FieldCode = "100503",
                    Name = "Textile Design",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Textile Design is the study of designing and producing textiles.",
                    ParentFieldOfEducationId = 396
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 399,
                    FieldCode = "100505",
                    Name = "Fashion Design",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Fashion Design is the study of creatively combining line, form and fabric in designing and constructing fashion garments.",
                    ParentFieldOfEducationId = 396
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 400,
                    FieldCode = "100599",
                    Name = "Graphic and Design Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Graphic and Design Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 396
                },
                #endregion
                #region Communication and Media Studies
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 401,
                    FieldCode = "1007",
                    Name = "Communication and Media Studies",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Communication and Media Studies is the study of creating, producing, disseminating and evaluating messages.",
                    ParentFieldOfEducationId = 383
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 402,
                    FieldCode = "100701",
                    Name = "Audio Visual Studies",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Audio Visual Studies is the study of producing films and videos, and television and radio programmes.",
                    ParentFieldOfEducationId = 401
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 403,
                    FieldCode = "100703",
                    Name = "Journalism",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Journalism is the study of researching current affairs and events and other matters of interests and reporting them.",
                    ParentFieldOfEducationId = 401
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 404,
                    FieldCode = "100705",
                    Name = "Written Communication",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Written Communication is the study of developing effective written communication skills.",
                    ParentFieldOfEducationId = 401
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 405,
                    FieldCode = "100707",
                    Name = "Verbal Communication",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Verbal Communication is the study of developing effective verbal communication skills.",
                    ParentFieldOfEducationId = 401
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 406,
                    FieldCode = "100799",
                    Name = "Communication and Media Studies, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Communication and Media Studies not elsewhere classified.",
                    ParentFieldOfEducationId = 401
                },
                #endregion
                #region Other Creative Arts
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 407,
                    FieldCode = "1099",
                    Name = "Other Creative Arts",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Creative Arts not elsewhere classified.",
                    ParentFieldOfEducationId = 383
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 408,
                    FieldCode = "109999",
                    Name = "Creative Arts, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Creative Arts not elsewhere classified.",
                    ParentFieldOfEducationId = 407
                },
                #endregion
                #endregion
                #region Food, Hospitality and Personal Services
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 409,
                    FieldCode = "11",
                    Name = "Food, Hospitality and Personal Services",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Food, Hospitality and Personal Services is the study of preparing, displaying and serving food and beverages, providing hospitality services, caring for the hair and body for grooming and beautification, and other personal services."
                },
                #region Food and Hospitality
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 410,
                    FieldCode = "1101",
                    Name = "Food and Hospitality",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Food and Hospitality is the study of preparing, presenting and serving food and beverages, and providing hospitality services.",
                    ParentFieldOfEducationId = 409
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 411,
                    FieldCode = "110101",
                    Name = "Hospitality",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Hospitality is the study of providing reception, accommodation, entertainment, food, beverages and related services to patrons at hotels, motels, clubs, restaurants, caravan parks, etc.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 412,
                    FieldCode = "110103",
                    Name = "Food and Beverage Service",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Food and Beverage Service is the study of serving and presenting food and beverages.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 413,
                    FieldCode = "110105",
                    Name = "Butchery",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Butchery is the study of cutting and preparing meat. It includes small-scale sausage and smallgoods making.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 414,
                    FieldCode = "110107",
                    Name = "Baking and Pastrymaking",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Baking and Pastrymaking is the study of making and presenting breads, pastries, buns and cakes.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 415,
                    FieldCode = "110109",
                    Name = "Cookery",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Cookery is the study of preparing food. It includes the development of recipes.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 416,
                    FieldCode = "110111",
                    Name = "Food Hygiene",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Food Hygiene is the study of the principles and regulations of food safe handling and preparation.",
                    ParentFieldOfEducationId = 410
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 417,
                    FieldCode = "110199",
                    Name = "Food and Hospitality, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Food and Hospitality not elsewhere classified.",
                    ParentFieldOfEducationId = 410
                },
                #endregion
                #region Personal Services
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 418,
                    FieldCode = "1103",
                    Name = "Personal Services",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Personal Services is the study of caring for the hair and body for grooming and beautification.",
                    ParentFieldOfEducationId = 409
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 419,
                    FieldCode = "110301",
                    Name = "Beauty Therapy",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Beauty Therapy is the study of maintaining and beautifying the face and body.",
                    ParentFieldOfEducationId = 418
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 420,
                    FieldCode = "110303",
                    Name = "Hairdressing",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Hairdressing is the study of cutting, colouring and styling scalp and facial hair.",
                    ParentFieldOfEducationId = 418
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 421,
                    FieldCode = "110399",
                    Name = "Personal Services, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Personal Services not elsewhere classified.",
                    ParentFieldOfEducationId = 418
                },
                #endregion
                #endregion
                #region Mixed Field Programmes
                new UoFieldOfEducationModel() {
                    FieldOfEducationId = 422,
                    FieldCode = "12",
                    Name = "Mixed Field Programmes",
                    Type = UoFieldOfEducationType.Broad,
                    Description = "Mixed Field Programmes are programmes providing general and personal development education."
                },
                #region General Education Programmes
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 423,
                    FieldCode = "1201",
                    Name = "General Education Programmes",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "General Education Programmes develop general knowledge, skills and competencies required for education and learning.",
                    ParentFieldOfEducationId = 422
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 424,
                    FieldCode = "120101",
                    Name = "General Primary and Secondary Education Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "General Primary and Secondary Education Programmes develop general knowledge and skills through school education programmes. It includes secondary education programmes run in TAFEs and other similar institutions.",
                    ParentFieldOfEducationId = 423
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 425,
                    FieldCode = "120103",
                    Name = "Literacy and Numeracy Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Literacy and Numeracy Programmes develop basic reading, writing and numeracy skills.",
                    ParentFieldOfEducationId = 423
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 426,
                    FieldCode = "120105",
                    Name = "Learning Skills Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Learning Skills Programmes develop skills for study, such as research and analysis skills.",
                    ParentFieldOfEducationId = 423
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 427,
                    FieldCode = "120199",
                    Name = "General Education Programmes, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all General Education Programmes not elsewhere classified.",
                    ParentFieldOfEducationId = 423
                },
                #endregion
                #region Social Skills Programmes
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 428,
                    FieldCode = "1203",
                    Name = "Social Skills Programmes",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Social Skills Programmes develop skills required for day to day interacting and functioning in society.",
                    ParentFieldOfEducationId = 422
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 429,
                    FieldCode = "120301",
                    Name = "Social and Interpersonal Skills Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Social and Interpersonal Skills Programmes develop skills for interacting with family and people in the wider community.",
                    ParentFieldOfEducationId = 428
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 430,
                    FieldCode = "120303",
                    Name = "Survival Skills Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Survival Skills Programmes develop skills for managing a household.",
                    ParentFieldOfEducationId = 428
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 431,
                    FieldCode = "120305",
                    Name = "Parental Education Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Parental Education Programmes develop skills for parenting.",
                    ParentFieldOfEducationId = 428
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 432,
                    FieldCode = "120399",
                    Name = "Social Skills Programmes, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Social Skills Programmes not elsewhere classified.",
                    ParentFieldOfEducationId = 428
                },
                #endregion
                #region Employment Skills Programmes
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 433,
                    FieldCode = "1205",
                    Name = "Employment Skills Programmes",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "Employment Skills Programmes develop job searching and employment related skills.",
                    ParentFieldOfEducationId = 422
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 434,
                    FieldCode = "120501",
                    Name = "Career Development Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Career Development Programmes provide guidance and counselling for career development.",
                    ParentFieldOfEducationId = 433
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 435,
                    FieldCode = "120503",
                    Name = "Job Search Skills Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Job Search Skills Programmes develop skills for finding a job.",
                    ParentFieldOfEducationId = 433
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 436,
                    FieldCode = "120505",
                    Name = "Work Practices Programmes",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "Work Practices Programmes develop skills for functioning effectively in the workplace.",
                    ParentFieldOfEducationId = 433
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 437,
                    FieldCode = "120599",
                    Name = "Employment Skills Programmes, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Employment Skills Programmes not elsewhere classified.",
                    ParentFieldOfEducationId = 433
                },
                #endregion
                #region Other Mixed Field Programmes
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 438,
                    FieldCode = "1299",
                    Name = "Other Mixed Field Programmes",
                    Type = UoFieldOfEducationType.Narrow,
                    Description = "This narrow field includes all Mixed Field Programmes not elsewhere classified.",
                    ParentFieldOfEducationId = 422
                },
                new UoFieldOfEducationModel()
                {
                    FieldOfEducationId = 439,
                    FieldCode = "129999",
                    Name = "Mixed Field Programmes, n.e.c.",
                    Type = UoFieldOfEducationType.Detailed,
                    Description = "This detailed field includes all Mixed Field Programmes not elsewhere classified.",
                    ParentFieldOfEducationId = 438
                }
                #endregion
                #endregion
            );
        }
    }
}
