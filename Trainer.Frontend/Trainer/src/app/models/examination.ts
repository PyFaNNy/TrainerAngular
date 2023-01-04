import {Patient} from "./patient";

export class Examination {
  id? : string;
  patientId? : string;
  typePhysicalActive?: number;
  date: Date = new Date();
  indicators?: number;
  status? : number;
  patient?: Patient;
}
