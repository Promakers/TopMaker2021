[linux]
export FLASK_ENV=development
export FLASK_APP=app_mask.py
flask run --host 0.0.0.0
python test_send.py

[windows]
set FLASK_ENV=development
set FLASK_APP=app_mask.py
flask run --host 0.0.0.0
python test_send.py

[PC TEST]
curl -v -X POST -F file=@"maksssksksss48.png" http://211.250.175.87:5100/predict
