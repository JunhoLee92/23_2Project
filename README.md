# Aardel

## :ribbon: 소개

2D 병합 타워 디펜스 입니다. 아기자기한 캐릭터들을 배치하여 적을 막아보세요.
![스크린샷](https://github.com/JunhoLee92/23_2Project/blob/main/Assets/ScreenShot/1703078098_19832889.png)

## :camera: 스크린샷

![스크린샷](https://github.com/JunhoLee92/23_2Project/blob/main/Assets/ScreenShot/1703078120_71557551.png)
![1](https://github.com/24AardelSEProjectTeam/AardelSEPJ/assets/116086980/a28521ca-accd-429a-83d2-055d40223432)
![2](https://github.com/24AardelSEProjectTeam/AardelSEPJ/assets/116086980/2d354522-117c-4c3b-8764-b760e14a1b81)
![스크린샷](https://github.com/JunhoLee92/23_2Project/blob/main/Assets/ScreenShot/1703078402_12561189.png)

## :video_camera: 동영상

게임 플레이 데모 동영상입니다.

[YouTube에서 데모 동영상 보기](https://youtu.be/CImKp8mz5OM?si=9x8Mgv2Pp_zxKDLg)

## :pushpin: 게임 개요
- 게임 이름: Aardel(아르델)
- 장르: 머지 디펜스 (병합, 타워디펜스)
- 플랫폼: Android / iOS
- 대상:
    - 머지 장르를 선호하는 유저층
    - 타워 디펜스 장르를 선호하는 유저층
    - 귀여운 캐릭터들의 수집을 선호하는 유저층

## :dart: 기획 의도
- 각 유닛들의 장단점, 특이점과 함께
유닛들 상호간의 시너지 효과, 배치
효과 등으로 매 판, 매 상황 마다
다른 플레이를 유도하는 것

## :video_game: 게임 플레이

게임 플레이 방법

- 게임 루프
- 캐릭터 병합 라운드
- 몬스터 웨이브 라운드
- 보스 라운드


- 게임 메뉴얼 참고
    - [GAME_MANUAL](GAME_MANUAL.md)

## :game_die: 핵심 기능

- 캐릭터 병합 시스템
- 독특한 턴 제 시스템
- 캐릭터 별 전투 시스템

## :floppy_disk: 사용된 기술

- Unity
- C#

## :hash: 참여인원

- Game Director

  - 기획 양진영
  - 서브기획 권상헌

- Game Programmer

  - 프로그래밍 이준호

- Graphic Designer
  - 아트디렉터 이승록
  - 캐릭터 일러스트 서현우
  - 캐릭터 심볼 신예림
  - 배경 이소원



# 23_2Project
 
2학기 프로젝트 _ Hive 

유니티버전: 2021.3.20f1 -> 2022.3.13f1 으로 변경

-진행중인 스크립트-

``##23-11-16``
BaseHealth - 본진 HP 관련 스크립트
GameManager
	주요매서드
	UnitEvolutionData[] 유닛 관련 데이터 관리 
	RoundConfig[] 라운드 정보 관리
	MonsterSpawnInfo 라운드별로 다른 몬스터 정보 관리(RoundConfig에 포함)
	OnMonsterSpawned() 몬스터 스폰 관련 매서드
	InitGrid() 유닛 자동 생성/랜덤배치
	OnUnitClicked() 유닛 병합 관련 매서드

IDamageable - 데미지 인터페이스 *일반 몬스터/보스 공통 적용

RoundRewardSystem -  라운드 마다의 보상 시스템 (뼈대만)

UIManager  - UI 관리하는 스크립트

유닛 관련 스크립트
Unit - 유닛 정보, 유닛관련 함수 호출
LilyAttack - 릴리 캐릭터 스크립트
UnitAttack - 칼리 캐릭터 스크립트
YukiAttack -유키 캐릭터 공격 스크립트
Projectile - 유키 투사체 관련 스크립트

몹 관련 스크립트
BossController -보스 관련 스크립트
BossSpawner -보스 스폰 관련 스크립트
LookAtCenter -몬스터가 중앙을 향할 수 있도록 조종하는 스크립트
MonsterController - 몬스터 관련 스크립트
MonsterSponer - 몬스터 스폰 관련 스크립트


씬 체인지 스크립트
Exit 게임 종료 스크립트
SceneChanger1~3 씬 체인지 스크립트 
