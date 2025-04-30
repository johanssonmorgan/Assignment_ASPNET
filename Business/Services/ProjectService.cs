
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;

namespace Business.Services;

public interface IProjectService
{
    Task<ProjectResult> CreateProjectAsync(AddProjectForm formData);
    Task<ProjectResult> DeleteProjectAsync(string id);
    Task<ProjectResult<Project>> GetProjectAsync(string id);
    Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync();
    Task<ProjectResult> UpdateProjectAsync(EditProjectForm formData);
}

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<ProjectResult> CreateProjectAsync(AddProjectForm formData)
    {
        if (formData == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields are supplied." };

        var projectEntity = formData.MapTo<ProjectEntity>();

        var result = await _projectRepository.AddAsync(projectEntity);

        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 201 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<ProjectResult<IEnumerable<Project>>> GetProjectsAsync()
    {
        var response = await _projectRepository.GetAllAsync
            (
                orderByDescending: true,
                sortBy: s => s.Created,
                where: null,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        return new ProjectResult<IEnumerable<Project>> { Succeeded = true, StatusCode = 200, Result = response.Result };
    }
    public async Task<ProjectResult<Project>> GetProjectAsync(string id)
    {
        var response = await _projectRepository.GetAsync
            (
                where: x => x.Id == id,
                include => include.User,
                include => include.Status,
                include => include.Client
            );

        return response.Succeeded
            ? new ProjectResult<Project> { Succeeded = true, StatusCode = 200, Result = response.Result }
            : new ProjectResult<Project> { Succeeded = false, StatusCode = 404, Error = $"Project '{id}' was not found." };
    }

    public async Task<ProjectResult> UpdateProjectAsync(EditProjectForm formData)
    {
        if (formData == null)
            return new ProjectResult { Succeeded = false, StatusCode = 400, Error = "Invalid form data." };

        var existingProjectResult = await _projectRepository.GetEntityAsync(formData.Id);

        if (!existingProjectResult.Succeeded)
            return new ProjectResult { Succeeded = false, StatusCode = 404, Error = "Project not found." };

        var projectEntity = existingProjectResult.Result!;

        projectEntity.ProjectName = formData.ProjectName;
        projectEntity.Description = formData.Description;
        projectEntity.Budget = formData.Budget;
        projectEntity.StartDate = formData.StartDate;
        projectEntity.EndDate = formData.EndDate;
        projectEntity.ClientId = formData.ClientId;
        projectEntity.StatusId = formData.StatusId;
        projectEntity.UserId = formData.UserId;

        if (!string.IsNullOrEmpty(formData.Image))
        {
            projectEntity.Image = formData.Image;
        }

        var result = await _projectRepository.UpdateAsync(projectEntity);

        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 200 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }



    public async Task<ProjectResult> DeleteProjectAsync(string id)
    {
        var existingEntity = await _projectRepository.GetEntityAsync(id);
        if (!existingEntity.Succeeded || existingEntity.Result == null)
        {
            return new ProjectResult
            {
                Succeeded = false,
                StatusCode = 404,
                Error = $"Project with ID '{id}' not found."
            };
        }

        var result = await _projectRepository.DeleteAsync(existingEntity.Result);
        return result.Succeeded
            ? new ProjectResult { Succeeded = true, StatusCode = 200 }
            : new ProjectResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }
}