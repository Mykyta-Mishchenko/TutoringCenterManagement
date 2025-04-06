import { Injectable, signal } from "@angular/core";
import { Teacher } from "../../../../shared/models/teacher.model";
import { Subject } from "../../../../shared/models/subject.model";

@Injectable({
    providedIn: 'root'
})
export class TeachersService {
  teachersList = signal<Teacher[]>(SAMPLE_TEACHERS_LIST);
  
  subjects = signal<Subject[]>(SAMPLE_SUBJECTS);
}

const SAMPLE_SUBJECTS: Subject[] = [
  { subjectId: 1, name: 'Mathematics' , classYear: 1},
  { subjectId: 2, name: 'Science', classYear: 1 },
  { subjectId: 3, name: 'History', classYear: 1 },
  { subjectId: 4, name: 'Literature', classYear: 1 },
  { subjectId: 5, name: 'Computer Science', classYear: 1 },
  { subjectId: 6, name: 'Physics', classYear: 1 },
  { subjectId: 7, name: 'Chemistry', classYear: 1 },
  { subjectId: 8, name: 'Biology', classYear: 1 },
  { subjectId: 9, name: 'Geography', classYear: 1 },
  { subjectId: 10, name: 'Art', classYear: 1 }
]

const SAMPLE_TEACHERS_LIST: Teacher[] = [
  {
    id: 1,
    fullName: "Dr. Sarah Johnson",
    profileImgUrl: null,
    subjects: [
      { subjectId: 101, name: "Math", classYear: 10 },
      { subjectId: 102, name: "Physics", classYear: 8 }
    ]
  },
  {
    id: 2,
    fullName: "Prof. Michael Chen",
    profileImgUrl: null,
    subjects: [
      { subjectId: 201, name: "Chemistry", classYear: 7 }
    ]
  },
  {
    id: 3,
    fullName: "Ms. Emily Wilson",
    profileImgUrl: null,
    subjects: [
      { subjectId: 301, name: "English", classYear: 6 },
      { subjectId: 302, name: "English", classYear: 9 },
      { subjectId: 303, name: "Ukrainian literature", classYear: 11 }
    ]
  },
  {
    id: 4,
    fullName: "Mr. David Rodriguez",
    profileImgUrl: null,
    subjects: [
      { subjectId: 401, name: "Biology", classYear: 7 },
      { subjectId: 402, name: "Chemistry", classYear: 10 }
    ]
  },
  {
    id: 5,
    fullName: "Dr. Lisa Park",
    profileImgUrl: null,
    subjects: [
      { subjectId: 501, name: "Physics", classYear: 9 },
      { subjectId: 502, name: "Math", classYear: 6 }
    ]
  },
  {
    id: 6,
    fullName: "Mr. James Wilson",
    profileImgUrl: null,
    subjects: [
      { subjectId: 601, name: "Ukrainian", classYear: 5 },
      { subjectId: 602, name: "Ukrainian literature", classYear: 8 }
    ]
  },
  {
    id: 7,
    fullName: "Ms. Olivia Martinez",
    profileImgUrl: null,
    subjects: [
      { subjectId: 701, name: "English", classYear: 7 },
      { subjectId: 702, name: "English", classYear: 12 }
    ]
  },
  {
    id: 8,
    fullName: "Prof. Robert Kim",
    profileImgUrl: null,
    subjects: [
      { subjectId: 801, name: "Math", classYear: 11 },
      { subjectId: 802, name: "Physics", classYear: 7 },
      { subjectId: 803, name: "Chemistry", classYear: 9 }
    ]
  },
  {
    id: 9,
    fullName: "Dr. Patricia Brown",
    profileImgUrl: null,
    subjects: [
      { subjectId: 901, name: "Biology", classYear: 8 }
    ]
  },
  {
    id: 10,
    fullName: "Mr. Thomas Lee",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1001, name: "Ukrainian", classYear: 6 },
      { subjectId: 1002, name: "Ukrainian literature", classYear: 10 }
    ]
  },
  {
    id: 11,
    fullName: "Ms. Jennifer Adams",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1101, name: "Math", classYear: 5 },
      { subjectId: 1102, name: "Math", classYear: 8 }
    ]
  },
  {
    id: 12,
    fullName: "Dr. William Taylor",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1201, name: "Physics", classYear: 10 },
      { subjectId: 1202, name: "Chemistry", classYear: 12 }
    ]
  },
  {
    id: 13,
    fullName: "Prof. Elizabeth Clark",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1301, name: "English", classYear: 7 },
      { subjectId: 1302, name: "Ukrainian", classYear: 9 }
    ]
  },
  {
    id: 14,
    fullName: "Mr. Daniel White",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1401, name: "Biology", classYear: 6 },
      { subjectId: 1402, name: "Biology", classYear: 11 }
    ]
  },
  {
    id: 15,
    fullName: "Dr. Nancy Hall",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1501, name: "Math", classYear: 9 },
      { subjectId: 1502, name: "Physics", classYear: 7 }
    ]
  },
  {
    id: 16,
    fullName: "Mr. Christopher Allen",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1601, name: "Ukrainian literature", classYear: 8 }
    ]
  },
  {
    id: 17,
    fullName: "Ms. Margaret Scott",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1701, name: "English", classYear: 10 },
      { subjectId: 1702, name: "Ukrainian", classYear: 7 }
    ]
  },
  {
    id: 18,
    fullName: "Prof. Richard King",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1801, name: "Chemistry", classYear: 8 },
      { subjectId: 1802, name: "Biology", classYear: 12 }
    ]
  },
  {
    id: 19,
    fullName: "Dr. Karen Wright",
    profileImgUrl: null,
    subjects: [
      { subjectId: 1901, name: "Physics", classYear: 6 },
      { subjectId: 1902, name: "Math", classYear: 11 },
      { subjectId: 1903, name: "Chemistry", classYear: 9 }
    ]
  },
  {
    id: 20,
    fullName: "Mr. Steven Baker",
    profileImgUrl: null,
    subjects: [
      { subjectId: 2001, name: "Ukrainian literature", classYear: 7 },
      { subjectId: 2002, name: "English", classYear: 12 }
    ]
  }
];