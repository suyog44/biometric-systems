#include <TutorialUtils.h>

#include <NCore.h>
#include <NCluster.h>

const NChar title[] = N_T("ServerDatabase");
const NChar description[] = N_T("Demonstrates how to use Accelerator database");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

const char defaultServerIp[] = "127.0.0.1";
const char defaultServerPort[] = "24932";

typedef enum _server_database_task_type
{
	sdttInsert = 0,
	sdttDelete = 1,
} server_database_task_type_e;

static int template_insert(cluster_socket_t *sock,
		const char *id, size_t id_length,
		const void *template_data, size_t template_size);
static int template_insert_start_task(cluster_socket_t *sock,
		const char *id, int id_length,
		const void *template_data, int template_size);
static int template_insert_standard_template(cluster_socket_t *sock,
		const char *id, size_t id_length,
		const void *template_data, size_t template_size, cluster_standard_template_type_e template_type);
static int template_insert_standard_template_start_task(cluster_socket_t *sock,
		const char *id, int id_length,
		const void *template_data, int template_size, cluster_standard_template_type_e template_type);
static int template_insert_wait_result(cluster_socket_t *sock, int task_id);
static int template_delete(cluster_socket_t *sock,
		const char *id, int id_length);
static int template_delete_start_task(cluster_socket_t *sock,
		const char *id, int id_length);
static int template_delete_wait_result(cluster_socket_t *sock, int task_id);

/**
 * Show usage screen to user.
 */
static int usage()
{
	printf("usage:\n");
	printf("\t%s -s [server:port] -c [command] -i [template id] -t [template] -y [template type]\n", title);
	printf("\n");
	printf("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)\n");
	printf("\t-c command       - command to be performed (either insert or delete) (required)\n");
	printf("\t-i template id   - id of template to be deleted or inserted (required)\n");
	printf("\t-t template      - template to be inserted (required only for insert)\n");
	printf("\t-y template type - type of template to be inserted (ansi or iso) (valid for insert to MegaMatcher Accelerator only) (optional)\n");
	printf("\t%s -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp \n", title);
	printf("\t%s -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp -y ansi\n", title);
	printf("\t%s -s 127.0.0.1:24932 -c delete -i testId\n", title);

	return 1;
}

#define CHECKOPT() \
{ \
	if (optarg == NULL) \
{ \
	parse_error = 1; \
	goto breakloop; \
} \
	c++; \
}

/**
* Parses arguments from command line.
*/
static int
parse_args(int argc, char **argv, char **srv_ip, char **srv_port, server_database_task_type_e *taskType, char **id, char **fileName, int *isStandardTemplate, cluster_standard_template_type_e *templateType)
{
	int c;
	int parse_error = 0;
	char *separator;

	if (argv == NULL || srv_ip == NULL || srv_port == NULL || fileName == NULL || taskType == NULL ||
		id == NULL || templateType == NULL || isStandardTemplate == NULL)
	{
		return CLUSTER_ARGUMENT_ERROR;
	}
	
	*srv_ip = (char*) malloc(strlen(defaultServerIp) + 1);
	strncpy(*srv_ip, defaultServerIp, strlen(defaultServerIp));
	(*srv_ip)[strlen(defaultServerIp)] = '\0';

	*srv_port = (char*) malloc(strlen(defaultServerPort) + 1);
	strncpy(*srv_port, defaultServerPort, strlen(defaultServerPort));
	(*srv_port)[strlen(defaultServerPort)] = '\0';

	*taskType = sdttInsert;
	*id = NULL;
	*fileName = NULL;
	*isStandardTemplate = 0;
	*templateType		= CLUSTER_TEMPLATE_ANSI;

	for (c = 1; c < argc; c++)
	{
		char *optarg = NULL;

		if (strlen(argv[c]) != 2 || argv[c][0] != '-')
		{
			parse_error = 1;
			break;
		}

		if (argc > c+1 && argv[c+1][0] != '-')
		{
			optarg = argv[c+1]; // we have a parameter for given flag
		}

		switch (argv[c][1])
		{
			case 's':
				CHECKOPT();
				separator = strchr(optarg, ':');
				if (separator)
				{
					size_t addressLength;
					size_t portLength;

					addressLength = separator - optarg;
					portLength = strlen(optarg) - addressLength - 1;
					*srv_ip = (char*) malloc(addressLength + 1);
					strncpy(*srv_ip, optarg, addressLength);
					(*srv_ip)[addressLength] = '\0';
					*srv_port = (char*) malloc(portLength + 1);
					strncpy(*srv_port, optarg + addressLength + 1, portLength);
					(*srv_port)[portLength] = '\0';
				}
				else
				{
					*srv_ip = (char*) malloc(strlen(optarg) + 1);
					strncpy(*srv_ip, optarg, strlen(optarg));
					(*srv_ip)[strlen(optarg)] = '\0';
				}
				break;
			case 'c':
				CHECKOPT();
				if (_stricmp(optarg, N_T("insert")) == 0)
				{
					*taskType = sdttInsert;
				}
				else if (_stricmp(optarg, N_T("delete")) == 0)
				{
					*taskType = sdttDelete;
				}
				else
				{
					printf(N_T("wrong command (should be insert or delete)!"));
					parse_error = 1;
					goto breakloop;
				}
				break;
			case 'i':
				CHECKOPT();
				*id = optarg;
				break;
			case 't':
				CHECKOPT();
				*fileName = optarg;
				break;

			case 'y':
				CHECKOPT();
				if (_stricmp(optarg, N_T("ansi")) == 0)
				{
					*templateType = CLUSTER_TEMPLATE_ANSI;
					*isStandardTemplate = 1;
				}
				else if (_stricmp(optarg, N_T("iso")) == 0)
				{
					*templateType = CLUSTER_TEMPLATE_ISO;
					*isStandardTemplate = 1;
				}
				else
				{
					printf(N_T("wrong standard (should be iso or ansi)!"));
					parse_error = 1;
					goto breakloop;
				}
				break;

			default:
				parse_error = 1;
				goto breakloop;
		}

	} /*   while   */

breakloop:

	printf("selecting task type: %s\n", (*taskType == sdttInsert) ? "insert" : "delete");

	if (*id == NULL)
	{
		printf("id - required parameter - not specified\n");
		parse_error = 1;
	}

	if (*taskType == sdttInsert)
	{
		if (*fileName == NULL)
		{
			printf("id - required parameter - not specified\n");
			parse_error = 1;
		}
	}

	if (parse_error)
	{
		usage();
		return CLUSTER_ARGUMENT_ERROR;
	}

	return CLUSTER_OK;
}

/**
 * Main database usage demonstration code
 */
int
main(int argc, char *argv[])
{
	NResult res;
	cluster_socket_t *sock = NULL;
	cluster_status_code_e ret;
	char *server;
	char *port;
	server_database_task_type_e taskType;
	char *id;
	size_t id_len;
	char *fileName;
	int isStandardTemplate = 0;
	cluster_standard_template_type_e template_type;
	void *template_data = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	res = parse_args(argc, argv, &server, &port, &taskType, &id, &fileName, &isStandardTemplate, &template_type);
	if (res != CLUSTER_OK)
	{
		printf("could not parse args\n");
		res = N_E_FAILED;
		goto FINALLY;
	}

	ret = cluster_initialize();
	if (ret != CLUSTER_OK)
	{
		printf("cluster_initialize failed with error %d\n", ret);
		res = ret;
		goto FINALLY;
	}

	sock = cluster_sock_connect(&ret, server, port);
	if (sock == NULL || ret != CLUSTER_OK)
	{
		printf("could not connect to server\n");
		res = ret;
		goto FINALLY;
	}

	id_len = strlen(id);

	if (taskType == sdttInsert)
	{
		NSizeType template_size;
		HNBuffer hBuffer = NULL;

		res = NFileReadAllBytesCN(fileName, &hBuffer);
		if (NFailed(res))
		{
			printf("count not read data from file %s (error = %d)\n", fileName, res);
			goto FINALLY;
		}
		res = NBufferToPtr(hBuffer, &template_size, &template_data);
		NObjectSet(NULL, &hBuffer);
		if (NFailed(res))
		{
			printf("failed to get template from buffer (error = %d)\n", res);
			goto FINALLY;
		}

		if (isStandardTemplate == 0)
		{
			res = template_insert(sock, id, id_len, template_data, template_size);
		}
		else
		{
			res = template_insert_standard_template(sock, id, id_len, template_data, template_size, template_type);
		}
		if (res < 0)
		{
			goto FINALLY;
		}
	}
	else if (taskType == sdttDelete)
	{
		res = template_delete(sock, id, (int)id_len);
		if (res < 0)
		{
			goto FINALLY;
		}
	}
	else
	{
		res = usage();
		goto FINALLY;
	}

	res = N_OK;

FINALLY:
	if (template_data)
		free(template_data);

	if (sock)
		cluster_sock_close(sock);

	cluster_finalize();

	OnExit();

	return res;
}

/*
 * insert
 */

static int
template_insert(cluster_socket_t *sock,
		const char *id, size_t id_length,
		const void *template_data, size_t template_size)
{
	int task_id;
	int res = 0;

	printf("starting template insert request ...\n");

	task_id = template_insert_start_task(sock,
			id, (int)id_length,
			template_data, (int)template_size);
	if (task_id < 0)
	{
		printf("failed to send task\n");
		goto failed;
	}

	printf("waiting for result ...\n");

	while ((res = template_insert_wait_result(sock, task_id)) == 0)
	{
		NThreadSleep(300);
	}

	if (res < 0)
	{
		printf("insert failed\n");
	}
	else
	{
		printf("insert succeeded.\n");
	}

cleanup:
	return res;

failed:
	res = -1;
	goto cleanup;
}

static int
template_insert_standard_template(cluster_socket_t *sock,
		const char *id, size_t id_length,
		const void *template_data, size_t template_size, cluster_standard_template_type_e template_type)
{
	int task_id;
	int res = 0;

	printf("starting template insert request ...\n");

	task_id = template_insert_standard_template_start_task(sock,
			id, (int)id_length,
			template_data, (int)template_size, template_type);
	if (task_id < 0)
	{
		printf("failed to send task\n");
		goto failed;
	}

	printf("waiting for result ...\n");

	while ((res = template_insert_wait_result(sock, task_id)) == 0)
	{
		NThreadSleep(300);
	}

	if (res < 0)
	{
		printf("insert failed\n");
	}
	else
	{
		printf("insert succeeded\n");
	}

cleanup:
	return res;

failed:
	res = -1;
	goto cleanup;
}

static int
template_insert_start_task(cluster_socket_t *sock,
		const char *id, int id_length,
		const void *template_data, int template_size)
{
	int task_id = -1;
	cluster_status_code_e ret;
	admin_handle_t *admin_handle = NULL;
	admin_handle_t *receive_handle = NULL;

	admin_handle = initialize_admin_handle();

	ret = cluster_admin_insert_db_records(admin_handle, 1,
			&id_length, &id,
			&template_size, &template_data);
	if (ret != CLUSTER_OK)
	{
		printf("failed to create insert command. return: %d\n", ret);
		goto failed;
	}

	ret = cluster_packet_send(admin_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		goto failed;
	}

	receive_handle = initialize_admin_handle();
	if (ret != CLUSTER_OK)
	{
		printf("could not create packet for receiving\n");
		goto failed;
	}

	ret = cluster_packet_recv(receive_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not received\n");
		goto failed;
	}

	ret = cluster_admin_get_insert_task_id(receive_handle, &task_id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to retrieve task id\n");
		goto failed;
	}

cleanup:
	if (admin_handle != NULL)
	{
		finalize_admin_handle(admin_handle);
		admin_handle = NULL;
	}

	if (receive_handle != NULL)
	{
		finalize_admin_handle(receive_handle);
		receive_handle = NULL;
	}

	return task_id;

failed:
	task_id = -1;
	goto cleanup;
}

static int
template_insert_standard_template_start_task(cluster_socket_t *sock,
		const char *id, int id_length,
		const void *template_data, int template_size, cluster_standard_template_type_e template_type)
{
	int task_id = -1;
	cluster_status_code_e ret;
	admin_handle_t *admin_handle = NULL;
	admin_handle_t *receive_handle = NULL;

	admin_handle = initialize_admin_handle();

	ret = cluster_admin_insert_standard_template_db_records(admin_handle, 1,
			&id_length, &id,
			&template_size, &template_data, &template_type);
	if (ret != CLUSTER_OK)
	{
		printf("failed to create insert command. return: %d\n", ret);
		goto failed;
	}

	ret = cluster_packet_send(admin_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		goto failed;
	}

	receive_handle = initialize_admin_handle();
	if (ret != CLUSTER_OK)
	{
		printf("could not create packet for receiving\n");
		goto failed;
	}

	ret = cluster_packet_recv(receive_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not received\n");
		goto failed;
	}

	ret = cluster_admin_get_insert_task_id(receive_handle, &task_id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to retrieve task id\n");
		goto failed;
	}

cleanup:
	if (admin_handle != NULL)
	{
		finalize_admin_handle(admin_handle);
		admin_handle = NULL;
	}

	if (receive_handle != NULL)
	{
		finalize_admin_handle(receive_handle);
		receive_handle = NULL;
	}

	return task_id;

failed:
	task_id = -1;
	goto cleanup;
}

static int
template_insert_wait_result(cluster_socket_t *sock, int task_id)
{
	int result = -1;
	cluster_status_code_e ret;
	admin_handle_t *admin_handle = NULL;
	admin_handle_t *receive_handle = NULL;
	int error_length;
	char *error_string;
	int i;
	int results_count;
	int status;

	admin_handle = initialize_admin_handle();

	ret = cluster_admin_db_insert_result_request(admin_handle, task_id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to create insert command. return: %d\n", ret);
		goto failed;
	}

	ret = cluster_packet_send(admin_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		goto failed;
	}

	receive_handle = initialize_admin_handle();
	if (ret != CLUSTER_OK)
	{
		printf("could not create packet for receiving\n");
		goto failed;
	}

	ret = cluster_packet_recv(receive_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not received\n");
		goto failed;
	}

	ret = cluster_admin_get_insert_task_result(receive_handle, &result);
	if (ret != CLUSTER_OK)
	{
		printf("failed to retrieve insert status\n");
		goto failed;
	}

	printf("insert result: %d ", result);
	if (result == CLUSTER_INSERT_DELETE_WAITING)
	{
		printf("(waiting)\n");
	}
	else if (result == CLUSTER_INSERT_DELETE_SUCCEEDED)
	{
		printf("(succeeded)\n");
	}
	else if ((result == CLUSTER_INSERT_DELETE_PARTIALLY_SUCCEEDED) || (result < 0))
	{
		switch (result)
		{
			case CLUSTER_INSERT_DELETE_PARTIALLY_SUCCEEDED:
				printf("(partial success)\n");
				break;
			case CLUSTER_INSERT_DELETE_FAILED:
				printf("(failed)\n");
				break;
			case CLUSTER_INSERT_DELETE_SERVER_NOT_READY:
				printf("(server is nor yet ready)\n");
				break;
			default:
				printf("(unknown failure status)\n");
				break;
		}

		ret = cluster_admin_get_insert_task_batch_size(receive_handle, &results_count);
		if (ret != CLUSTER_OK)
		{
			printf("failed to retrieve batch size of task\n");
			goto failed;
		}

		for (i = 0; i < results_count; i++)
		{
			ret = cluster_admin_get_insert_task_status(receive_handle, i, &status);
			if (ret != CLUSTER_OK)
			{
				printf("failed to retrieve batch size of task\n");
				goto failed;
			}

			printf("template %d status: %d ", i, status);
			if (status == 0)
			{
				printf("(ok)\n");
			}
			else
			{
				printf("(error)\n");

				ret = cluster_admin_get_insert_task_error(receive_handle, i, &error_length, &error_string);
				if (ret != CLUSTER_OK)
				{
					printf("failed to retrieve insert error info\n");
					goto failed;
				}

				if (error_string != NULL
						&& error_length > 0)
				{
					printf("insert error: %s\n", error_string);
				}
			}
		}
	}
	else
	{
		printf("(unknown status)\n");
	}

cleanup:
	if (admin_handle != NULL)
	{
		finalize_admin_handle(admin_handle);
		admin_handle = NULL;
	}

	if (receive_handle != NULL)
	{
		finalize_admin_handle(receive_handle);
		receive_handle = NULL;
	}

	return result;

failed:
	ret = -1;
	goto cleanup;
}

/*
 * delete
 */

static int
template_delete(cluster_socket_t *sock,
		const char *id, int id_length)
{
	int task_id;
	int res = 0;

	printf("starting template delete request ...\n");

	task_id = template_delete_start_task(sock,
			id, id_length);
	if (task_id < 0)
	{
		printf("failed to send task\n");
		goto failed;
	}

	printf("waiting for result ...\n");

	while ((res = template_delete_wait_result(sock, task_id)) == 0)
	{
		NThreadSleep(300);
	}

	if (res < 0)
	{
		printf("delete failed\n");
	}
	else
	{
		printf("delete succeeded.\n");
	}

cleanup:
	return res;

failed:
	res = -1;
	goto cleanup;
}

static int
template_delete_start_task(cluster_socket_t *sock,
		const char *id, int id_length)
{
	int task_id = -1;
	cluster_status_code_e ret;
	admin_handle_t *admin_handle = NULL;
	admin_handle_t *receive_handle = NULL;

	admin_handle = initialize_admin_handle();

	ret = cluster_admin_delete_db_records(admin_handle, 1,
			&id_length, &id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to create delete command. Return: %d\n", ret);
		goto failed;
	}

	ret = cluster_packet_send(admin_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		goto failed;
	}

	receive_handle = initialize_admin_handle();
	if (ret != CLUSTER_OK)
	{
		printf("could not create packet for receiving\n");
		goto failed;
	}

	ret = cluster_packet_recv(receive_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not received\n");
		goto failed;
	}

	ret = cluster_admin_get_delete_task_id(receive_handle, &task_id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to retrieve task id\n");
		goto failed;
	}

cleanup:
	if (admin_handle != NULL)
	{
		finalize_admin_handle(admin_handle);
		admin_handle = NULL;
	}

	if (receive_handle != NULL)
	{
		finalize_admin_handle(receive_handle);
		receive_handle = NULL;
	}

	return task_id;

failed:
	task_id = -1;
	goto cleanup;
}

static int
template_delete_wait_result(cluster_socket_t *sock, int task_id)
{
	int result = -1;
	cluster_status_code_e ret;
	admin_handle_t *admin_handle = NULL;
	admin_handle_t *receive_handle = NULL;
	int error_length;
	char *error_string;
	int i;
	int results_count;
	int status;

	admin_handle = initialize_admin_handle();

	ret = cluster_admin_db_delete_result_request(admin_handle, task_id);
	if (ret != CLUSTER_OK)
	{
		printf("failed to create delete command. Return: %d\n", ret);
		goto failed;
	}

	ret = cluster_packet_send(admin_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not sent\n");
		goto failed;
	}

	receive_handle = initialize_admin_handle();
	if (ret != CLUSTER_OK)
	{
		printf("could not create packet for receiving\n");
		goto failed;
	}

	ret = cluster_packet_recv(receive_handle, sock);
	if (ret != CLUSTER_OK)
	{
		printf("packet not received\n");
		goto failed;
	}

	ret = cluster_admin_get_delete_task_result(receive_handle, &result);
	if (ret != CLUSTER_OK)
	{
		printf("failed to retrieve delete status\n");
		goto failed;
	}

	printf("delete result: %d ", result);
	if (result == CLUSTER_INSERT_DELETE_WAITING)
	{
		printf("(waiting)\n");
	}
	else if (result == CLUSTER_INSERT_DELETE_SUCCEEDED)
	{
		printf("(succeeded)\n");
	}
	else if ((result == CLUSTER_INSERT_DELETE_PARTIALLY_SUCCEEDED) || (result == CLUSTER_INSERT_DELETE_FAILED))
	{
		if (result == CLUSTER_INSERT_DELETE_PARTIALLY_SUCCEEDED)
		{
			printf("(partial success)\n");
		}
		else
		{
			printf("(failed)\n");
		}

		ret = cluster_admin_get_delete_task_batch_size(receive_handle, &results_count);
		if (ret != CLUSTER_OK)
		{
			printf("failed to retrieve batch size of task\n");
			goto failed;
		}

		for (i = 0; i < results_count; i++)
		{
			ret = cluster_admin_get_delete_task_status(receive_handle, i, &status);
			if (ret != CLUSTER_OK)
			{
				printf("failed to retrieve batch size of task\n");
				goto failed;
			}

			printf("template %d status: %d ", i, status);
			if (status == 0)
			{
				printf("(ok)\n");
			}
			else
			{
				printf("(error)\n");

				ret = cluster_admin_get_delete_task_error(receive_handle, i, &error_length, &error_string);
				if (ret != CLUSTER_OK)
				{
					printf("failed to retrieve delete error info\n");
					goto failed;
				}

				if (error_string != NULL
						&& error_length > 0)
				{
					printf("delete error: %s\n", error_string);
				}
			}
		}
	}
	else
	{
		printf("(unknown status)\n");
	}

cleanup:
	if (admin_handle != NULL)
	{
		finalize_admin_handle(admin_handle);
		admin_handle = NULL;
	}

	if (receive_handle != NULL)
	{
		finalize_admin_handle(receive_handle);
		receive_handle = NULL;
	}

	return result;

failed:
	ret = -1;
	goto cleanup;
}
