for /F "tokens=2-4 delims=/ " %%i in ('date /t') do set yyyymmdd1="%%k"_"%%i"_"%%j"
mkdir .\\report\\%yyyymmdd1%

locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 1m -c 300 -r 100 --csv=./report/%yyyymmdd1%/test_v1_1 --logfile test.log
locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 1m -c 300 -r 100 --csv=./report/%yyyymmdd1%/test_v1_2 --logfile test.log
REM locust -f ./dashboard/get_dashboard_v1.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v1_3
REM locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_1
REM locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_2
REM locust -f ./dashboard/get_dashboard_v2.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/dashboard_v2_3

REM locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_message_1
REM locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_message_2
REM locust -f ./announcement/API_get_message.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_message_3
REM locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_permission_1
REM locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_permission_2
REM locust -f ./announcement/API_get_permission.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_permission_3
REM locust -f ./announcement/API_get_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_unseen_count_1
REM locust -f ./announcement/API_get_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_unseen_count_2
REM locust -f ./announcement/API_get_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_ann_unseen_count_3
REM locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_ack_1
REM locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_ack_2
REM locust -f ./announcement/API_put_ack.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_ack_3
REM locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_read_1
REM locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_read_2
REM locust -f ./announcement/API_put_read.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_put_read_3

REM locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_lesson_1
REM locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_lesson_2
REM locust -f ./learning/API_get_course_lesson.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_lesson_3
REM locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_list_1
REM locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_list_2
REM locust -f ./learning/API_get_course_list.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_course_list_3
REM locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_lesson_page_1
REM locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_lesson_page_2
REM locust -f ./learning/API_get_lesson_page.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_lesson_page_3

REM locust -f ./login/login.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/login_1
REM locust -f ./login/login.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/login_2
REM locust -f ./login/login.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/login_3

REM locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_event_reward_1
REM locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_event_reward_2
REM locust -f ./reward/API_get_event_reward.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_event_reward_3
REM locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_history_1
REM locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_history_2
REM locust -f ./reward/API_get_reward_history.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_history_3
REM locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_reason_1
REM locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_reason_2
REM locust -f ./reward/API_get_reward_reason.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_reason_3
REM locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_summary_1
REM locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_summary_2
REM locust -f ./reward/API_get_reward_summary.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_summary_3
REM locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_receiver_1
REM locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_receiver_2
REM locust -f ./reward/API_get_top_receiver.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_receiver_3
REM locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_sender_1
REM locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_sender_2
REM locust -f ./reward/API_get_top_sender.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_top_sender_3
REM locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_unseen_count_1
REM locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_unseen_count_2
REM locust -f ./reward/API_get_reward_unseen_count.py --no-web --run-time 3m -c 3000 -r 100 --csv=./report/%yyyymmdd1%/API_get_reward_unseen_count_3

REM pause

