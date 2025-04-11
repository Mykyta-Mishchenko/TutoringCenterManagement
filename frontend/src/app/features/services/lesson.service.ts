import { Injectable } from '@angular/core';
import { Subject } from '../../shared/models/subject.model';
import { Lesson } from '../../shared/models/lesson.model';
import { LessonDTO } from '../../shared/models/dto/lesson-create.dto';

@Injectable({
  providedIn: 'root'
})
export class LessonService{

    getSubjects(): Subject[] {
        return SAMPLE_SUBJECTS;
    }

    getUserSchedule(teacherId: number): Lesson[]{
        return SAMPLE_TEACHER_SCHEDULE;
    }

    updateLesson(lesson: LessonDTO): Lesson {
        return {} as Lesson;
    }

    addLesson(lesson: LessonDTO): Lesson {
        return {} as Lesson;
    }

    unsubscribeLesson(lessonId: number) {
    
    }
    
    subscribeLesson(lessonId: number) {
    
    }
}

const SAMPLE_SUBJECTS: Subject[] = [
  { subjectId: 101, name: "Math"},
  { subjectId: 102, name: "Physics"},
  { subjectId: 103, name: "English"},
  { subjectId: 104, name: "Ukrainian" },
  { subjectId: 105, name: "Chemistry" },
  { subjectId: 106, name: "Biology" }
];

const SAMPLE_TEACHER_SCHEDULE: Lesson[] = [
  {
      lessonId: 1,
      teacherId: 101,
      type: {
          typeId: 1001,
          maxStudentsCount: 3,
          subject: { subjectId: 101, name: "Math" },
          schoolYear: 11,
          price: 500
      },
      schedule: { dateId: 4001, dayOfWeek: 1, dayTime: new Date(0, 0, 0, 14, 0) },
      studentsCount: 2
  },
  {
      lessonId: 2,
      teacherId: 101,
      type: {
          typeId: 1002,
          maxStudentsCount: 1,
          subject: { subjectId: 102, name: "Physics" },
          schoolYear: 10,
          price: 800
      },
      schedule: { dateId: 4002, dayOfWeek: 2, dayTime: new Date(0, 0, 0, 11, 30) },
      studentsCount: 1
  },
  {
      lessonId: 3,
      teacherId: 101,
      type: {
          typeId: 1003,
          maxStudentsCount: 4,
          subject: { subjectId: 105, name: "Chemistry" },
          schoolYear: 12,
          price: 600
      },
      schedule: { dateId: 4003, dayOfWeek: 3, dayTime: new Date(0, 0, 0, 16, 45) },
      studentsCount: 3
  },
  {
      lessonId: 4,
      teacherId: 101,
      type: {
          typeId: 1004,
          maxStudentsCount: 5,
          subject: { subjectId: 103, name: "English" },
          schoolYear: 9,
          price: 450
      },
      schedule: { dateId: 4004, dayOfWeek: 4, dayTime: new Date(0, 0, 0, 9, 15) },
      studentsCount: 5
  },
  {
      lessonId: 5,
      teacherId: 101,
      type: {
          typeId: 1005,
          maxStudentsCount: 2,
          subject: { subjectId: 106, name: "Biology" },
          schoolYear: 10,
          price: 550
      },
      schedule: { dateId: 4005, dayOfWeek: 6, dayTime: new Date(0, 0, 0, 17, 30) },
      studentsCount: 1
  },
  {
      lessonId: 6,
      teacherId: 101,
      type: {
          typeId: 1006,
          maxStudentsCount: 3,
          subject: { subjectId: 101, name: "Math" },
          schoolYear: 12,
          price: 650
      },
      schedule: { dateId: 4006, dayOfWeek: 1, dayTime: new Date(0, 0, 0, 19, 0) },
      studentsCount: 3
  },
  {
      lessonId: 7,
      teacherId: 101,
      type: {
          typeId: 1007,
          maxStudentsCount: 4,
          subject: { subjectId: 102, name: "Physics" },
          schoolYear: 11,
          price: 700
      },
      schedule: { dateId: 4007, dayOfWeek: 2, dayTime: new Date(0, 0, 0, 13, 45) },
      studentsCount: 1
  },
  {
      lessonId: 8,
      teacherId: 101,
      type: {
          typeId: 1008,
          maxStudentsCount: 5,
          subject: { subjectId: 103, name: "English" },
          schoolYear: 10,
          price: 480
      },
      schedule: { dateId: 4008, dayOfWeek: 3, dayTime: new Date(0, 0, 0, 10, 0) },
      studentsCount: 3
  },
  {
      lessonId: 9,
      teacherId: 101,
      type: {
          typeId: 1009,
          maxStudentsCount: 2,
          subject: { subjectId: 106, name: "Biology" },
          schoolYear: 11,
          price: 520
      },
      schedule: { dateId: 4009, dayOfWeek: 4, dayTime: new Date(0, 0, 0, 20, 15) },
      studentsCount: 2
  },
  {
      lessonId: 10,
      teacherId: 101,
      type: {
          typeId: 1010,
          maxStudentsCount: 2, 
          subject: { subjectId: 101, name: "Math" },
          schoolYear: 9,
          price: 400
      },
      schedule: { dateId: 4010, dayOfWeek: 5, dayTime: new Date(0, 0, 0, 15, 20) },
      studentsCount: 0
  }
];
