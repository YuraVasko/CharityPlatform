import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateCharityProjectDto } from 'src/app/models/organization';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  pathPrefix = "api/charity/projects";

  constructor(private http: HttpClient) {
  }

  createProject(
    title: string,
    description: string,
    charityOrganizationId: string,
    donationGoal: number,
    startDate: Date,
    endDate: Date
  ) {
    return this.http.post<CreateCharityProjectDto>("http://localhost:4200/" + this.pathPrefix,
      {
        title: title,
        description: description,
        charityOrganizationId: charityOrganizationId,
        donationGoal: donationGoal,
        startDate: startDate,
        endDate: endDate
      });
  }

  getProjects() {
    return this.http.get<CreateCharityProjectDto>("http://localhost:4200/" + this.pathPrefix);
  }
}
