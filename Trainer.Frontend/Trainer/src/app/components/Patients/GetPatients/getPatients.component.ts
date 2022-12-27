import {Component, OnInit} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";

@Component({
  selector: 'app-getPatients',
  templateUrl: './getPatients.component.html'
})

export class GetPatientsComponent implements OnInit {
  patients: Patient[] = [];
  patientToEdit?: Patient;

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.patientService
      .getPatients()
      .subscribe((result: any) => ( this.patients = result.Items));
  }

  // updateHeroList(heroes: SuperHero[]) {
  //   this.heroes = heroes;
  // }
  //
  // initNewHero() {
  //   this.heroToEdit = new SuperHero();
  // }
  //
  // editHero(hero: SuperHero) {
  //   this.heroToEdit = hero;
  // }
}
