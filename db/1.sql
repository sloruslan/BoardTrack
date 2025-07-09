create table "PRODUCTION_STEP"
(
    id           bigint primary key,
    created_at   timestamp default timezone('utc'::text, now()) not null,
    updated_at   timestamp default timezone('utc'::text, now()) not null,
    is_active    boolean   default true,

    name         varchar not null
);

INSERT INTO public."PRODUCTION_STEP"
    (id, created_at, updated_at, is_active, name)
VALUES
    (1, now(), now(), true, 'Регистрация'),
    (2, now(), now(), true, 'Установка компонентов'),
    (3, now(), now(), true, 'Контроль качества'),
    (4, now(), now(), true, 'Ремонт'),
    (5, now(), now(), true, 'Упаковывание');

create table "BOARD_TYPE"
(
    id           bigint generated always as identity
        primary key,
    created_at   timestamp default timezone('utc'::text, now()) not null,
    updated_at   timestamp default timezone('utc'::text, now()) not null,
    is_active    boolean   default true,

    name         varchar not null,
    description      text
);

create table "BOARD"
(
    id           bigint generated always as identity,
    created_at   timestamp default timezone('utc'::text, now()) not null,
    updated_at   timestamp default timezone('utc'::text, now()) not null,
    is_active    boolean   default true,

    name         varchar not null,
    serial       varchar not null,
    type_id         bigint not null references public."BOARD_TYPE" (id),
    current_step_id smallint not null references public."PRODUCTION_STEP" (id),
    constraint "PK_BOARD"
        primary key (id, created_at)

) PARTITION BY RANGE (created_at);
CREATE TABLE "BOARD_DEFAULT" PARTITION OF "BOARD" default;


create table "PRODUCTION_STEP_RULE"
(
    id              bigint primary key,
    created_at      timestamp default timezone('utc'::text, now()) not null,
    updated_at      timestamp default timezone('utc'::text, now()) not null,
    is_active       boolean   default true,

    current_step_id smallint not null references public."PRODUCTION_STEP" (id),
    valid_next_step_id   smallint not null references public."PRODUCTION_STEP" (id),
    description     varchar
);

INSERT INTO public."PRODUCTION_STEP_RULE"
(id, created_at, updated_at, is_active, current_step_id, valid_next_step_id, description)
VALUES
    (1, now(), now(), true, 1, 2, ''),
    (2, now(), now(), true, 2, 3, ''),
    (3, now(), now(), true, 3, 4, ''),
    (4, now(), now(), true, 4, 3, ''),
    (5, now(), now(), true, 3, 5, ''),

    (6, now(), now(), true, 1, 1, ''),
    (7, now(), now(), true, 2, 2, ''),
    (8, now(), now(), true, 3, 3, ''),
    (9, now(), now(), true, 4, 4, ''),
    (10, now(), now(), true, 5, 5, '');

