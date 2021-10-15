using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

using UoN.AccrediTool.Core.Data;
using UoN.AccrediTool.Core.Models;

namespace UoN.AccrediTool.Core.Repositories
{
    public class UoProgramRepository : IUoProgramRepository
    {
        private readonly DataContext _context;
        public UoProgramRepository(DataContext context)
        {
            _context = context;
        }

        public List<UoProgramModel> GetAllPrograms()
        {
            List<UoProgramModel> programItems = _context.Program.ToList();
            return programItems;
        }

        public async Task<UoProgramModel> GetProgramById(int id)
        {
            try
            {
                UoProgramModel programItem = await _context.Program
                    .Include(program => program.ProgramStructure)
                        .ThenInclude(courseList => courseList.CourseListCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.FieldOfEducation)
                    .Include(program => program.LearningOutcomes)
                    .Include(program => program.Documents)
                    .Include(program => program.LevelPrograms)
                        .ThenInclude(join => join.Level)
                        .ThenInclude(level => level.LevelCategory)
                    .Include(program => program.LevelPrograms)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(program => program.NodePrograms)
                        .ThenInclude(join => join.Node)
                        .ThenInclude(node => node.Framework)
                    .Include(program => program.Campus)
                    .Include(program => program.ProgramCareer)
                    .SingleAsync(program => program.ProgramId == id)
                    .ConfigureAwait(false);

                return programItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Program.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<UoProgramModel> GetProgramMappingById(int id)
        {
            try
            {
                UoProgramModel programItem = await _context.Program
                    .Include(program => program.ProgramStructure)
                        .ThenInclude(courseList => courseList.CourseListCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.LevelCourses)
                        .ThenInclude(join => join.Level)
                        .ThenInclude(level => level.LevelCategory)
                    .Include(program => program.ProgramStructure)
                        .ThenInclude(courseList => courseList.CourseListCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.LevelCourses)
                        .ThenInclude(join => join.Node)
                    .Include(program => program.ProgramStructure)
                        .ThenInclude(courseList => courseList.CourseListCourses)
                        .ThenInclude(join => join.Course)
                        .ThenInclude(course => course.NodeCourses)
                        .ThenInclude(join => join.Node)
                    .SingleAsync(program => program.ProgramId == id)
                    .ConfigureAwait(false);

                return programItem;
            }
            catch (System.InvalidOperationException)
            {
                return await _context.Program.FindAsync(id).ConfigureAwait(false);
            }
        }

        public async Task<int?> AddProgram(UoProgramModel program)
        {
            if (program is UoProgramModel)
            {
                program.ProgramId = 0;
                _context.Program.Add(program);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return program.ProgramId;
            }
            return null;
        }

        public async Task<int?> SetProgramById(int id, UoProgramModel newProgram)
        {
            if (await _context.Program.FindAsync(id).ConfigureAwait(false) is UoProgramModel oldProgram)
            {
                if (newProgram is UoProgramModel)
                {
                    newProgram.ProgramId = oldProgram.ProgramId;
                    _context.Entry(oldProgram).CurrentValues.SetValues(newProgram);
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> UpdateProgramById(int id, JObject patchObject)
        {
            if (await _context.Program.FindAsync(id).ConfigureAwait(false) is UoProgramModel program)
            {
                if (patchObject != null)
                {
                    foreach (JProperty property in patchObject.Properties())
                    {
                        switch (property.Name)
                        {
                            case "programCode":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int programCode = property.Value.ToObject<int>();
                                    if (programCode >= 0)
                                    {
                                        program.ProgramCode = programCode;
                                    }
                                }
                                break;
                            case "name":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    program.Name = property.Value.ToString();
                                }
                                break;
                            case "firstTermCode":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int firstTermCode = property.Value.ToObject<int>();
                                    if (firstTermCode > 0)
                                    {
                                        program.FirstTermCode = firstTermCode;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.FirstTermCode = null;
                                }
                                break;
                            case "CRICOScode":
                                if (property.Value.Type == JTokenType.String)
                                {
                                    program.CRICOSCode = property.Value.ToString();
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.CRICOSCode = null;
                                }
                                break;
                            case "minUnits":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int minUnits = property.Value.ToObject<int>();
                                    if (minUnits >= 0)
                                    {
                                        program.MinUnits = minUnits;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.MinUnits = null;
                                }
                                break;
                            case "duration":
                                if (property.Value.Type == JTokenType.Float)
                                {
                                    float duration = property.Value.ToObject<float>();
                                    if (duration > 0)
                                    {
                                        program.Duration = duration;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.Duration = null;
                                }
                                break;
                            case "maxYears":
                                if (property.Value.Type == JTokenType.Float)
                                {
                                    float maxYears = property.Value.ToObject<float>();
                                    if (maxYears > 0)
                                    {
                                        program.MaxYears = maxYears;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.MaxYears = null;
                                }
                                break;
                            case "campusId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int campusId = property.Value.ToObject<int>();
                                    if (campusId > 0)
                                    {
                                        program.CampusId = campusId;
                                    }
                                }
                                break;
                            case "programCareerId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int programCareerId = property.Value.ToObject<int>();
                                    if (programCareerId > 0)
                                    {
                                        program.ProgramCareerId = programCareerId;
                                    }
                                }
                                break;
                            case "fieldOfEducationId":
                                if (property.Value.Type == JTokenType.Integer)
                                {
                                    int fieldOfEducationId = property.Value.ToObject<int>();
                                    if (fieldOfEducationId > 0)
                                    {
                                        program.FieldOfEducationId = fieldOfEducationId;
                                    }
                                }
                                else if (property.Value.Type == JTokenType.Null)
                                {
                                    program.FieldOfEducationId = null;
                                }
                                break;
                            default:
                                return 0;
                        }
                    }
                    return await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return 0;
            }
            return null;
        }

        public async Task<int?> DeleteProgramById(int id)
        {
            if (await _context.Program.FindAsync(id).ConfigureAwait(false) is UoProgramModel program)
            {
                _context.Program.Remove(program);
                return await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return null;
        }
    }
}
