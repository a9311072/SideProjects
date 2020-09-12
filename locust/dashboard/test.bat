for /F "tokens=2-4 delims=/ " %%i in ('date /t') do set yyyymmdd1="%%k"_"%%i"_"%%j"
mkdir ..\\report\\%yyyymmdd1%
locust -f dashboard_v1.py --no-web --run-time 10m -c 3000 -r 100 --csv=../report/%yyyymmdd1%/dashboard_v1_10m_1