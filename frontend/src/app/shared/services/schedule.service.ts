import { Injectable } from "@angular/core";
import { Lesson } from "../models/lesson.model";

@Injectable({
    providedIn: 'root'
})
export class ScheduleService{

    private teacherSchedule = SAMPLE_TEACHER_SCHEDULE;

    getTeacherChedule(teacherId: number): Lesson[]{
        return this.teacherSchedule;
    }
}

const SAMPLE_TEACHER_SCHEDULE: Lesson[] = [
    {
        lessonId: 1,
        teacherId: 101,
        type: {
            typeId: 1001,
            group: {
                groupId: 2001,
                name: "Math Group A",
                maxStudentsCount: 3
            },
            subject: {
                subjectId: 3001,
                name: "Mathematics",
                classYear: 11
            },
            price: 500
        },
        schedule: {
            dateId: 4001,
            dayOfWeek: 1, // Monday
            dayTime: new Date(0, 0, 0, 14, 0) // 14:00 (2 PM)
        },
        studentsCount: 2
    },
    {
        lessonId: 2,
        teacherId: 101,
        type: {
            typeId: 1002,
            group: {
                groupId: 2002,
                name: "Physics Individual",
                maxStudentsCount: 1
            },
            subject: {
                subjectId: 3002,
                name: "Physics",
                classYear: 10
            },
            price: 800
        },
        schedule: {
            dateId: 4002,
            dayOfWeek: 2, // Tuesday
            dayTime: new Date(0, 0, 0, 11, 30) // 11:30 AM
        },
        studentsCount: 1
    },
    {
        lessonId: 3,
        teacherId: 101,
        type: {
            typeId: 1003,
            group: {
                groupId: 2003,
                name: "Chemistry Group",
                maxStudentsCount: 4
            },
            subject: {
                subjectId: 3003,
                name: "Chemistry",
                classYear: 12
            },
            price: 600
        },
        schedule: {
            dateId: 4003,
            dayOfWeek: 3, // Wednesday
            dayTime: new Date(0, 0, 0, 16, 45) // 4:45 PM
        },
        studentsCount: 3
    },
    {
        lessonId: 4,
        teacherId: 101,
        type: {
            typeId: 1004,
            group: {
                groupId: 2004,
                name: "English Group",
                maxStudentsCount: 5
            },
            subject: {
                subjectId: 3004,
                name: "English",
                classYear: 9
            },
            price: 450
        },
        schedule: {
            dateId: 4004,
            dayOfWeek: 4, // Thursday
            dayTime: new Date(0, 0, 0, 9, 15) // 9:15 AM
        },
        studentsCount: 5
    },
    {
        lessonId: 5,
        teacherId: 101,
        type: {
            typeId: 1005,
            group: {
                groupId: 2005,
                name: "Biology Small Group",
                maxStudentsCount: 2
            },
            subject: {
                subjectId: 3005,
                name: "Biology",
                classYear: 10
            },
            price: 550
        },
        schedule: {
            dateId: 4005,
            dayOfWeek: 6, // Friday
            dayTime: new Date(0, 0, 0, 17, 30) // 5:30 PM
        },
        studentsCount: 1
    },
    {
        lessonId: 6,
        teacherId: 101,
        type: {
            typeId: 1006,
            group: {
                groupId: 2006,
                name: "Advanced Math",
                maxStudentsCount: 3
            },
            subject: {
                subjectId: 3001,
                name: "Mathematics",
                classYear: 12
            },
            price: 650
        },
        schedule: {
            dateId: 4006,
            dayOfWeek: 1, // Monday
            dayTime: new Date(0, 0, 0, 19, 0) // 7:00 PM
        },
        studentsCount: 3
    },
    {
        lessonId: 7,
        teacherId: 101,
        type: {
            typeId: 1007,
            group: {
                groupId: 2007,
                name: "Physics Group",
                maxStudentsCount: 4
            },
            subject: {
                subjectId: 3002,
                name: "Physics",
                classYear: 11
            },
            price: 700
        },
        schedule: {
            dateId: 4007,
            dayOfWeek: 2, // Tuesday
            dayTime: new Date(0, 0, 0, 13, 45) // 1:45 PM
        },
        studentsCount: 1
    },
    {
        lessonId: 8,
        teacherId: 101,
        type: {
            typeId: 1008,
            group: {
                groupId: 2008,
                name: "Literature Circle",
                maxStudentsCount: 5
            },
            subject: {
                subjectId: 3004,
                name: "English",
                classYear: 10
            },
            price: 480
        },
        schedule: {
            dateId: 4008,
            dayOfWeek: 3, // Wednesday
            dayTime: new Date(0, 0, 0, 10, 0) // 10:00 AM
        },
        studentsCount: 3
    },
    {
        lessonId: 9,
        teacherId: 101,
        type: {
            typeId: 1009,
            group: {
                groupId: 2009,
                name: "Science Club",
                maxStudentsCount: 2
            },
            subject: {
                subjectId: 3005,
                name: "Biology",
                classYear: 11
            },
            price: 520
        },
        schedule: {
            dateId: 4009,
            dayOfWeek: 4, // Thursday
            dayTime: new Date(0, 0, 0, 20, 15) // 8:15 PM
        },
        studentsCount: 2
    },
    {
        lessonId: 10,
        teacherId: 101,
        type: {
            typeId: 1010,
            group: {
                groupId: 2010,
                name: "Math Basics",
                maxStudentsCount: 2
            },
            subject: {
                subjectId: 3001,
                name: "Mathematics",
                classYear: 9
            },
            price: 400
        },
        schedule: {
            dateId: 4010,
            dayOfWeek: 5, // Friday
            dayTime: new Date(0, 0, 0, 15, 20) // 3:20 PM
        },
        studentsCount: 0
    }
];