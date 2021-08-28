## 시연 동영상

[![Watch the video](https://tistory2.daumcdn.net/tistory/2946431/skin/images/main_key_events_4005.png)](https://www.youtube.com/embed/h0MOsyAalx4)
* 이미지 클릭시 동영상이 재생 됩니다.


## Pro Makers (김도혁/유수엽/최윤규/권준호/강진호)

* 권준호 : 카메라 모듈 연동 작업 
* 최윤규 : 타이젠 UI 및 소스 머지 작업
* 김도혁 : 온도 센서 연동 및 모듈 구현
* 유수엽 : 온도 센서 하드웨어 분석  
* 강진호 : 기획 및 테스트

## 프로젝트 배경 혹은 목적

  <img src="https://user-images.githubusercontent.com/46912845/131213333-831025ad-cfd8-4ab7-9903-07f61f57fb85.JPG" width="80%"></img>
  <img src="https://user-images.githubusercontent.com/46912845/131213363-608d0e82-baed-48f6-909f-dc7a446c14c9.JPG" width="80%"></img>
  <img src="https://user-images.githubusercontent.com/46912845/131213365-8ead6984-0a1f-40b9-9af9-dfd34c7bd5ac.JPG" width="80%"></img>
  
## 파일 리스트

### 카메라 연동

  + App.xaml.cs  
  + https://github.com/Samsung/Tizen-CSharp-Samples/tree/master/Mobile/Lescanner 참조 하여 제작
  + C# Xamarin Bluetooth관련해서 참조할만한 소스가 1개 이며 포팅 후 기기 인식 부분에 문제 확인

### UI


### 비접촉 온도 센서

  + https://github.com/adafruit/Adafruit-MLX90614-Library 참조 
  + https://github.com/Promakers/TopMaker2021/blob/main/MLX90614/MLX90614.cs 타이젠 C# 클래스 제작
  

 ## 보드 
 
  * PI4  : Kiosk 화면 조작 및 관련 처리 
  * 센서 : 비 접촉 온도 센서 MLX90614
